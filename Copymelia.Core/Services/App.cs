using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class App 
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    
    public App(ILogger<App> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void Run()
    {
        ValidateConfiguration();
    }

    private void ValidateConfiguration()
    {
        var images = _configuration["images"];
        
        if(!Path.Exists(images))
            _logger.LogError($"Path {images} does not exist");
    }
}