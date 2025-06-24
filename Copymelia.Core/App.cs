using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core;

public class App : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    
    public App(ILogger<App> logger, IHostApplicationLifetime applicationLifetime)
    {
        _logger = logger; 
        _applicationLifetime = applicationLifetime;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Hello World!");
        _applicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}