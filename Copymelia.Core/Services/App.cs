using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class App 
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly FileProcessor _fileProcessor;
    private string _path;
    public App(ILogger<App> logger, IConfiguration configuration, FileProcessor fileProcessor)
    {
        _logger = logger;
        _configuration = configuration;
        _fileProcessor = fileProcessor;
    }

    public void Run()
    {
        ValidateConfiguration();
        _fileProcessor.Process(_path);
    }

    private void ValidateConfiguration()
    {
        var path = _configuration["path"];

        if(!Path.Exists(path))
            _logger.LogError($"Path {path} does not exist");
        
        _path = path;
        
    }
}