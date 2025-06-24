using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class App 
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly ImageProcessor _imageProcessor;
    private string _imagesDirectory;
    public App(ILogger<App> logger, IConfiguration configuration, ImageProcessor imageProcessor)
    {
        _logger = logger;
        _configuration = configuration;
        _imageProcessor = imageProcessor;
    }

    public void Run()
    {
        ValidateConfiguration();
        _imageProcessor.Process(_imagesDirectory);
    }

    private void ValidateConfiguration()
    {
        var images = _configuration["images"];

        if(!Path.Exists(images))
            _logger.LogError($"Path {images} does not exist");
        
        _imagesDirectory = images;
        
    }
}