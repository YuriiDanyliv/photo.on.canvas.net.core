using AutoMapper.QueryableExtensions;
using BLL.Mapper;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogLevel = InstagramApiSharp.Logger.LogLevel;

namespace POC.BLL.Services
{
    public class InstagramService : IInstagramService
    {
        private IInstaApi _instaApi;

        private readonly UserSessionData _userSession;
        private readonly IConfigurationService<InstagramServiceConfig> _instaConfig;
        private readonly ILogger<InstagramService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public InstagramService(
          IConfigurationService<InstagramServiceConfig> instaConfig,
          ILogger<InstagramService> logger,
          IUnitOfWork unitOfWork)
        {
            _instaConfig = instaConfig;
            _logger = logger;
            _userSession = new UserSessionData();
            _unitOfWork = unitOfWork;

            var data = _instaConfig.GetSettingsAsync().Result;
            if (data.Password != null || data.Username != null)
            {
                _userSession.UserName = data.Username;
                _userSession.Password = data.Password;
            }
            else
            {
                _logger.LogError("password or username empty");
            }

            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(_userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .Build();
        }

        /// <inheritdoc/>
        public async Task<IList<InstagramMediaDto>> GetStoriesAsync(string name)
        {
            var result = await _unitOfWork.InstaMedia
                .FindByCondition(x => x.Category == name)
                .ProjectTo<InstagramMediaDto>(ObjMapper.configuration)
                .ToListAsync();

            return (result);
        }

        /// <inheritdoc/>
        public async Task<IList<InstagramMediaDto>> GetStoriesAsync()
        {
            var result = await _unitOfWork.InstaMedia
                .FindAll()
                .ProjectTo<InstagramMediaDto>(ObjMapper.configuration)
                .ToListAsync();

            return (result);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task UpdateDbInstaDataAsync()
        {
            var stories = await GetArchiveStoriesUrlAsync();

            if (stories.Count == 0)
            {
                _logger.LogWarning("Instagram Media is not updated, no stories available");
                return;
            }

            foreach (var item in await _unitOfWork.InstaMedia.FindAll().ToListAsync())
            {
                _unitOfWork.InstaMedia.Delete(item);
            }

            foreach (var story in stories)
            {
                _unitOfWork.InstaMedia.Create(story);
            }

            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Instagram Media updated");
        }

        /// <summary>
        /// Sign in instagram
        /// </summary>
        /// <returns></returns>
        private async Task Login()
        {
            if (_instaApi.IsUserAuthenticated) return;

            var logInResult = await _instaApi.LoginAsync();
            if (!logInResult.Succeeded) _logger.LogWarning(logInResult.Info.Message.ToString());
        }

        /// <summary>
        /// Get saved stories from instagram
        /// </summary>
        /// <returns></returns>
        private async Task<List<InstagramMedia>> GetArchiveStoriesUrlAsync()
        {
            await Login();

            var mediaList = new List<InstagramMedia>();
            if (!_instaApi.IsUserAuthenticated) return mediaList;

            var user = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync("photo.on.canvas.ukraine");
            if (!user.Succeeded) _logger.LogError(user.Info.ToString());

            var stories = await _instaApi.StoryProcessor.GetHighlightFeedsAsync(user.Value.Pk);
            if (!stories.Succeeded) _logger.LogError(stories.Info.ToString());

            foreach (var item in stories.Value.Items)
            {
                var highlightMedia = await _instaApi.StoryProcessor.GetHighlightMediasAsync(item.HighlightId);
                if (!highlightMedia.Succeeded) _logger.LogError(highlightMedia.Info.ToString());

                foreach (var story in highlightMedia.Value.Items)
                {
                    mediaList.Add(
                      new InstagramMedia
                      {
                          Category = item.Title,
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