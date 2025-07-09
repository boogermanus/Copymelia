using Copymelia.Constants;
using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class OutputDirector
{
    private readonly ILogger<OutputDirector> _logger;

    public OutputDirector(ILogger<OutputDirector> logger)
    {
        _logger = logger;
    }
    
    public void Build(Options options)
    {
        if (!Path.Exists(options.Output))
        {
            _logger.LogInformation($"Creating output directory '{options.Output}'");
            Directory.CreateDirectory(options.Output);
        }
        
        var imagesPath = Path.Combine(options.Output, OutputDirectories.ImagesDirectory);

        if (!Path.Exists(imagesPath))
        {
            _logger.LogInformation($"Creating images directory '{imagesPath}'");
            Directory.CreateDirectory(imagesPath);
        }
        
        var documentsPath = Path.Combine(options.Output, OutputDirectories.DocumentsDirectory);

        if (!Path.Exists(documentsPath))
        {
            _logger.LogInformation($"Creating documents directory '{documentsPath}'");
            Directory.CreateDirectory(documentsPath);
        }
        
        var videosPath = Path.Combine(options.Output, OutputDirectories.VideosDirectory);

        if (!Path.Exists(videosPath))
        {
            _logger.LogInformation($"Creating videos directory '{videosPath}'");
            Directory.CreateDirectory(videosPath);
        }
        
        var audioPath = Path.Combine(options.Output, OutputDirectories.AudioDirectory);

        if (!Path.Exists(audioPath))
        {
            _logger.LogInformation($"Creating audio directory '{audioPath}'");
            Directory.CreateDirectory(audioPath);
        }
    }
}