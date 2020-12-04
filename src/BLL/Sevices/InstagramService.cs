using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Logging;
using POC.BLL.Model;
using POC.BLL.Models;
using LogLevel = InstagramApiSharp.Logger.LogLevel;

namespace POC.BLL.Services
{
  public class InstagramService : IInstagramService
  {
    private readonly IInstaApi _instaApi;
    private readonly IConfigurationService<InstagramServiceConfig> _instaConfig;
    private readonly ILogger _logger;
    private readonly UserSessionData _userSession;

    public InstagramService(
      IConfigurationService<InstagramServiceConfig> instaConfig,
      ILogger<InstagramService> logger
    )
    {
      _instaConfig = instaConfig;
      _logger = logger;
      _userSession = new UserSessionData();
      
      var data = instaConfig.GetSettingsAsync().Result;

      if (data.Password != null || data.Username != null)
      {
        _userSession.UserName = data.Username;
        _userSession.Password = data.Password;
      } 
      else 
      {
        throw new Exception("No instagram credential");
      }

      _instaApi = InstaApiBuilder.CreateBuilder()
      .SetUser(_userSession)
      .UseLogger(new DebugLogger(LogLevel.Exceptions))
      .Build();
    }

    private async Task Login()
    {
      if (!_instaApi.IsUserAuthenticated)
      {
        var logInResult = await _instaApi.LoginAsync();

        _logger.LogInformation("LoggIn", logInResult.Succeeded.ToString(), logInResult.Info);
      }
    }

    public async Task<List<InstagramMediaModel>> GetStoriesByNameAsync(string storyName)
    {
      await Login();

      var mediaList = new List<InstagramMediaModel>();
      var instaStoryList = new List<InstaStoryItem>();

      var user = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync(_userSession.UserName);
      if (!user.Succeeded) throw new Exception(user.Info.ToString());

      var stories = await _instaApi.StoryProcessor.GetHighlightFeedsAsync(user.Value.Pk);
      if (!stories.Succeeded) throw new Exception(stories.Info.ToString());

      foreach (var item in stories.Value.Items)
      {
        if (item.Title == storyName)
        {
          var highlightMedia = await _instaApi.StoryProcessor.GetHighlightMediasAsync(item.HighlightId);
          if (!highlightMedia.Succeeded) throw new Exception(highlightMedia.Info.ToString());
          instaStoryList = highlightMedia.Value.Items;
        }
      }

      foreach (var item in instaStoryList)
      {
        string image = null;
        string video = null;

        if (item.ImageList.Count != 0) image = item.ImageList[0].Uri;
        if (item.VideoList.Count != 0) video = item.VideoList[0].Uri;

        mediaList.Add(new InstagramMediaModel { ImageURL = image, VideoURL = video });
      }

      return mediaList;
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
  }
}