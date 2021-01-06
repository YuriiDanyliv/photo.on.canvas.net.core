using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace POC.BLL.Services
{
  public class InstagramHostedService : IHostedService
  {
    private Timer _timer;
    private readonly ILogger<InstagramHostedService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public InstagramHostedService(
      ILogger<InstagramHostedService> logger,
      IServiceScopeFactory scopeFactory
    )
    {
      _logger = logger;
      _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Insta Service Start Work");

      _timer = new Timer(Callback, null, TimeSpan.FromMinutes(0.1), TimeSpan.FromHours(1));
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _timer?.Change(Timeout.Infinite, 0);
      return Task.CompletedTask;
    }

    private async void Callback(object state)
    {
      using(var scope = _scopeFactory.CreateScope())
      {
        var instaService = scope.ServiceProvider.GetRequiredService<IInstagramService>();
        await instaService.UpdateDbInstaDataAsync();
      }
    }
  }
}