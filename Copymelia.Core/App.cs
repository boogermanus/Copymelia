using Microsoft.Extensions.Logging;

namespace Copymelia.Core;

public class App 
{
    private readonly ILogger _logger;
    
    public App(ILogger<App> logger)
    {
        _logger = logger; 
    }

    public void Run()
    {
        _logger.LogInformation("Hello World!");
    }
}