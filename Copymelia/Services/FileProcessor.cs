using Copymelia.Extensions;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{
    private readonly ZipFileProcessor _zipFileProcessor;
    public FileProcessor(ILogger<FileProcessor> logger, MoveDirector moveDirector, ZipFileProcessor zipFileProcessor) 
        : base(logger, moveDirector)
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
        }

        Logger.LogInformation($"Processed File: '{file}'");
    }
}