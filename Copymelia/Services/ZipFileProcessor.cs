using System.IO.Compression;
using Copymelia.Constants;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class ZipFileProcessor : FileProcessorBase
{
    private readonly ILogger<ZipFileProcessor> _logger;
    
    public ZipFileProcessor(ILogger<ZipFileProcessor> logger, MoveDirector moveDirector) : base(logger, moveDirector)
    {
        _logger = logger;
    }

    public void ProcessZipFile(string zipPath)
    {
        // extract
        var extractPath = Path.Combine(Path.Combine(Path.GetTempPath(), OutputDirectories.ZipTempDirectory),
            Path.GetFileNameWithoutExtension(zipPath));
        ZipFile.ExtractToDirectory(extractPath, zipPath);
        // remove zip file
        File.Delete(zipPath);
        // process
        var directories = Directory.EnumerateDirectories(extractPath);
        var files = Directory.EnumerateFiles(extractPath);
        ProcessDirectories(directories);
        ProcessFiles(files);
        // cleanup temp dir
        Directory.Delete(extractPath, true);
    }
}