using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class OutputDirector
{
    private readonly ILogger<OutputDirector> _logger;
    private readonly string _imagesDirectory = "images";
    private readonly string _documentsDirectory = "documents";
    private readonly string _videosDirectory = "videos";
    private readonly string _audioDirectory = "audio";
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
        
        var imagesPath = Path.Combine(options.Output, _imagesDirectory);

        if (!Path.Exists(imagesPath))
        {
            _logger.LogInformation($"Creating images directory '{imagesPath}'");
            Directory.CreateDirectory(imagesPath);
        }
        
        var documentsPath = Path.Combine(options.Output, _documentsDirectory);

        if (!Path.Exists(documentsPath))
        {
            _logger.LogInformation($"Creating documents directory '{documentsPath}'");
            Directory.CreateDirectory(documentsPath);
        }
        
        var videosPath = Path.Combine(options.Output, _videosDirectory);

        if (!Path.Exists(videosPath))
        {
            _logger.LogInformation($"Creating videos directory '{videosPath}'");
            Directory.CreateDirectory(videosPath);
        }
        
        var audioPath = Path.Combine(options.Output, _audioDirectory);

        if (!Path.Exists(audioPath))
        {
            _logger.LogInformation($"Creating audio directory '{audioPath}'");
            Directory.CreateDirectory(audioPath);
        }
    }
}