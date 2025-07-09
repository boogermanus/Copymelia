using System.IO.Compression;
using Copymelia.Constants;
using Copymelia.Extensions;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{
    private readonly MoveDirector _moveDirector;

    public FileProcessor(ILogger<FileProcessor> logger, MoveDirector moveDirector) : base(logger)
    {
        _moveDirector = moveDirector;
    }

    protected override void ProcessFile(string file)
    {
        var info = new FileInfo(file);

        if (info.IsImage())
        {
            Logger.LogInformation($"Identified image: {info.Name}");
            MoveFile(info, OutputDirectories.ImagesDirectory);
        }

        if (info.IsDocument())
        {
            Logger.LogInformation($"Identified document: {info.Name}");
            MoveFile(info, OutputDirectories.DocumentsDirectory);
        }

        if (info.IsVideo())
        {
            Logger.LogInformation($"Identified video: {info.Name}");
            MoveFile(info, OutputDirectories.VideosDirectory);
        }

        if (info.IsAudio())
        {
            Logger.LogInformation($"Identified music: {info.Name}");
            MoveFile(info, OutputDirectories.AudioDirectory);
        }

        if (info.IsZip())
            Logger.LogInformation($"Identified zip: {info.Name}");

        Logger.LogInformation($"Processed File: '{file}'");
    }

    private void MoveFile(FileInfo file, string destination)
    {
        var outputDirectory = Path.Combine(Options.Output, destination);
        if (Options.WhatIf)
        {
            Logger.LogInformation(
                $"WhatIf moving {file.FullName} to {Path.Combine(outputDirectory, file.Name)}");
        }
        else
        {
            _moveDirector.Move(file, outputDirectory);
        }
    }
}