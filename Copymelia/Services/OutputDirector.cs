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
    }
}