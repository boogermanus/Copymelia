using Copymelia.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class FileProcessor : FileProcessorBase
{
    private readonly ZipFileProcessor _zipFileProcessor;
    public FileProcessor(ILogger<FileProcessor> logger, OutputDirector outputDirector, ZipFileProcessor zipFileProcessor) 
        : base(logger, outputDirector)
    {
        _zipFileProcessor = zipFileProcessor;
    }

    protected override void ProcessFile(string file)
    {
        base.ProcessFile(file);
        var info = new FileInfo(file);

        if (info.IsZip() && info.Name.ToLower().Contains("backup"))
        {
            Logger.LogInformation($"Identified zip: {info.Name}");
            _zipFileProcessor.ProcessZipFile(Options, info.FullName);
            Files += _zipFileProcessor.Files;
        }

        Logger.LogInformation($"Processed File: '{file}'");
    }
}