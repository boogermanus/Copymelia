using System.IO.Compression;
using Copymelia.Core.Constants;
using Copymelia.Core.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class ZipFileProcessor : FileProcessorBase
{
    private readonly ILogger<ZipFileProcessor> _logger;

    public ZipFileProcessor(ILogger<ZipFileProcessor> logger, OutputDirector outputDirector) : base(logger, outputDirector)
    {
        _logger = logger;
    }

    public void ProcessZipFile(Options options, string zipPath)
    {
        Options = options;

        if (Options.WhatIf)
        {
            _logger.LogInformation($"WhatIf processing zip file {zipPath}");
            return;
        }
        // extract
        var extractPath = Path.Combine(Path.Combine(Path.GetTempPath(), OutputDirectories.ZipTempDirectory),
            Path.GetFileNameWithoutExtension(zipPath));
        if(!Path.Exists(extractPath))
            Directory.CreateDirectory(extractPath);
        ZipFile.ExtractToDirectory(zipPath, extractPath);
        _logger.LogInformation("Zip file extracted to '{OutputDirectory}'", extractPath);
        
        // remove zip file
        File.Delete(zipPath);
        _logger.LogInformation("Removed zipFile {zipFile}", zipPath);
        
        // process
        var directories = Directory.EnumerateDirectories(extractPath);
        var files = Directory.EnumerateFiles(extractPath);
        ProcessDirectories(directories);
        ProcessFiles(files);
        
        // cleanup temp dir
        Directory.Delete(extractPath, true);
        _logger.LogInformation("Removed directory {directory}", extractPath);
        
        // reset file counter
        Files = 0;
    }
}