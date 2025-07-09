using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class ZipFileProcessor : FileProcessorBase
{
    private readonly ILogger<ZipFileProcessor> _logger;
    
    public ZipFileProcessor(ILogger<ZipFileProcessor> logger, MoveDirector moveDirector) : base(logger, moveDirector)
    {
        _logger = logger;
    }

    public void ProcessZipFile(Options options, string zipPath)
    {
        
    }
}