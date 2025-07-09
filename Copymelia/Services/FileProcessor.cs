using System.IO.Compression;
using Copymelia.Constants;
using Copymelia.Extensions;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{
    private readonly MoveDirector _moveDirector;

    public FileProcessor(ILogger<FileProcessor> logger, MoveDirector moveDirector) : base(logger, moveDirector)
    {
        _moveDirector = moveDirector;
    }

    protected override void ProcessFile(string file)
    {
        base.ProcessFile(file);
        var info = new FileInfo(file);

        if (info.IsZip())
        {
            Logger.LogInformation($"Identified zip: {info.Name}");
        }

        Logger.LogInformation($"Processed File: '{file}'");
    }
}