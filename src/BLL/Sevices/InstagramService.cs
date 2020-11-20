using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public class InstagramService : IInstagramService
  {
    private readonly IInstaApi _instaApi;
    private readonly ConfigurationService<InstagramServiceConfig> _instaConfig;

    public InstagramService(
      ConfigurationService<InstagramServiceConfig> instaConfig
    )
    {
      _instaConfig = instaConfig;

      var userSession = new UserSessionData();
      userSession.UserName = instaConfig.GetSettingsAsync().Result.Username;
      userSession.Password = instaConfig.GetSettingsAsync().Result.Password;

      _instaApi = InstaApiBuilder.CreateBuilder()
      .SetUser(userSession)
      .UseLogger(new DebugLogger(LogLevel.Exceptions))
      .Build();
    }

    private async Task Login()
    {
      if (!_instaApi.IsUserAuthenticated)
      {
        var logInResult = await _instaApi.LoginAsync();
        if (logInResult.Succeeded) System.Console.WriteLine("logged In");
      }
    }

    public async Task GetStoriesAsync()
    {
      var storyList = await _instaApi.StoryProcessor.GetHighlightsArchiveAsync();
      //storyList.Value.Items.
    }

    public async Task WriteMessageAsync(string text)
    {
      var cfg = await _instaConfig.GetSettingsAsync();

      if (cfg.SendMsgTo.Count != 0)
      {
        foreach (var username in cfg.SendMsgTo)
        {
          var user = await _instaApi.UserProcessor.GetUserAsync(username);
          await _instaApi.MessagingProcessor.SendDirectTextAsync(user.Value.Pk.ToString(), null, text);
        }
      }
    }
  }
}