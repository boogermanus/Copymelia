using Microsoft.Extensions.Logging;

namespace Copymelia;

public class App
{
    private readonly ILogger<App> _logger;

    public App(ILogger<App> logger)
    {
        _logger = logger;
    }

    public void Run(string[] args)
    {
        var files = Directory.GetFiles(@"c:\users\booge\downloads");
        foreach (var file in files)
        {
            _logger.LogInformation("{File}", file);
        }
    }
}