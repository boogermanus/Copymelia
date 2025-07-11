using Copymelia.Constants;
using Copymelia.Extensions;
using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public abstract class FileProcessorBase
{
    protected ILogger Logger { get; }
    protected Options Options { get; set; }
    public int Files { get; set; }
    private readonly MoveDirector _moveDirector;

    public FileProcessorBase(ILogger logger, MoveDirector moveDirector)
    {
        Logger = logger;
        Options = new Options();
        _moveDirector = moveDirector;
    }
    
    public void Process(Options options)
    {
        Options = options;
        Logger.LogInformation($"Processing path '{options.Path}'");
        var directories = Directory.EnumerateDirectories(options.Path);
        var files = Directory.EnumerateFiles(options.Path);
        ProcessDirectories(directories);
        ProcessFiles(files);
        Logger.LogInformation($"Files processed: {Files}");
    }
    
    protected void ProcessDirectories(IEnumerable<string> directories)
    {
        foreach (var directory in directories)
        {
            Logger.LogInformation($"Processing directory '{directory}'");
            var subDirectories = Directory.GetDirectories(directory);
            ProcessDirectories(subDirectories);
            ProcessFiles(Directory.GetFiles(directory));
        }
    }
    
    protected void ProcessFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            Logger.LogInformation($"Processing file '{file}'");
            Files++;
            ProcessFile(file);
        }
    }

    protected virtual void ProcessFile(string file)
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
            switch (Options.Mode)
            {
                case Modes.Move:
                    _moveDirector.Move(file, outputDirectory);
                    break;
                case Modes.Copy:
                    _moveDirector.Move(file, outputDirectory, Modes.Copy);
                    break;
            }
        }
    }
    
}