using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POC.BLL.Model;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Interfaces;
using LogLevel = InstagramApiSharp.Logger.LogLevel;

namespace POC.BLL.Services
{
  public class InstagramService : IInstagramService
  {
    private IInstaApi _instaApi;
    private UserSessionData _userSession;

    private readonly IConfigurationService<InstagramServiceConfig> _instaConfig;
    private readonly ILogger<InstagramService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public InstagramService(
      IConfigurationService<InstagramServiceConfig> instaConfig,
      ILogger<InstagramService> logger,
      IUnitOfWork unitOfWork,
      IFileService fileService
    )
    {
      _instaConfig = instaConfig;
      _logger = logger;
      _userSession = new UserSessionData();
      _unitOfWork = unitOfWork;
      _fileService = fileService;
    }

    public async Task<IList<InstagramMedia>> GetStoriesAsync(string name)
    {
      return (await _unitOfWork.InstaMedia.FindByCondition(x => x.Category == name).ToListAsync());
    }

    public async Task<IList<InstagramMedia>> GetStoriesAsync()
    {
      return (await _unitOfWork.InstaMedia.FindAll().ToListAsync());
    }

    public async Task WriteMessageAsync(string text)
    {
      await Login();

      var cfg = await _instaConfig.GetSettingsAsync();

      if (cfg.MsgReceiverList.Count != 0)
      {
        foreach (var username in cfg.MsgReceiverList)
        {
          var user = await _instaApi.UserProcessor.GetUserAsync(username);
          await _instaApi.MessagingProcessor.SendDirectTextAsync(user.Value.Pk.ToString(), null, text);
        }
      }
    }

    public async Task UpdateDbInstaDataAsync()
    {
      var stories = await GetArchiveStoriesUrlAsync();

      if (stories.Count == 0) return;
      foreach (var item in _unitOfWork.InstaMedia.FindAll())
      {
        _unitOfWork.InstaMedia.Delete(item);
      }

      foreach (var story in stories)
      {
        var entity = new InstagramMedia();
        entity.Category = story.Title;
        entity.ImageUri = story.ImageUri;
        entity.VideoUri = story.VideoUri;

        _unitOfWork.InstaMedia.Create(entity);
      }

      await _unitOfWork.SaveAsync();
      _logger.LogInformation("Instagram Media updated");
    }

    private async Task Login()
    {
      var data = await _instaConfig.GetSettingsAsync();

      if (data.Password != null || data.Username != null)
      {
        _userSession.UserName = data.Username;
        _userSession.Password = data.Password;
      }
      else
      {
        throw new Exception("password or username empty");
      }

      _instaApi = InstaApiBuilder.CreateBuilder()
      .SetUser(_userSession)
      .UseLogger(new DebugLogger(LogLevel.Exceptions))
      .Build();

      if (!_instaApi.IsUserAuthenticated)
      {
        var logInResult = await _instaApi.LoginAsync();

        if (!logInResult.Succeeded) _logger.LogError("Fail to login", logInResult.Info.Message);
      }
    }

    private async Task<List<InstagramMediaModel>> GetArchiveStoriesUrlAsync()
    {
      await Login();

      var mediaList = new List<InstagramMediaModel>();

      var user = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync("photo.on.canvas.ukraine");
      if (!user.Succeeded) throw new Exception(user.Info.ToString());

      var stories = await _instaApi.StoryProcessor.GetHighlightFeedsAsync(user.Value.Pk);
      if (!stories.Succeeded) throw new Exception(stories.Info.ToString());

      foreach (var item in stories.Value.Items)
      {
        var highlightMedia = await _instaApi.StoryProcessor.GetHighlightMediasAsync(item.HighlightId);
        if (!highlightMedia.Succeeded) throw new Exception(highlightMedia.Info.ToString());

        foreach (var story in highlightMedia.Value.Items)
        {
          mediaList.Add(
            new InstagramMediaModel
            {
              StoryId = story.Id,
              Title = item.Title,
              ImageUri = story.ImageList.Count != 0 ? story.ImageList[0].Uri : null,
              VideoUri = story.VideoList.Count != 0 ? story.VideoList[0].Uri : null
            }
          );
        }
      }
      return mediaList;
    }
  }
}