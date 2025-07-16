using Copymelia.Core.Constants;
using Copymelia.Core.Extensions;
using Copymelia.Core.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public abstract class FileProcessorBase
{
    protected ILogger Logger { get; }
    protected Options Options { get; set; }
    public int Files { get; set; }
    private readonly OutputDirector _outputDirector;

    public FileProcessorBase(ILogger logger, OutputDirector outputDirector)
    {
        Logger = logger;
        Options = new Options();
        _outputDirector = outputDirector;
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
            try
            {
                var subDirectories = Directory.GetDirectories(directory);
                ProcessDirectories(subDirectories);
                ProcessFiles(Directory.GetFiles(directory));
            }
            catch (UnauthorizedAccessException)
            {
                Logger.LogError("Unable to process {dir} access denied", directory);
            }

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
            HandleFile(info, OutputDirectories.ImagesDirectory);
        }

        if (info.IsDocument())
        {
            Logger.LogInformation($"Identified document: {info.Name}");
            HandleFile(info, OutputDirectories.DocumentsDirectory);
        }

        if (info.IsVideo())
        {
            Logger.LogInformation($"Identified video: {info.Name}");
            HandleFile(info, OutputDirectories.VideosDirectory);
        }

        if (info.IsAudio())
        {
            Logger.LogInformation($"Identified music: {info.Name}");
            HandleFile(info, OutputDirectories.AudioDirectory);
        }
    }
    private void HandleFile(FileInfo file, string destination)
    {
        var outputDirectory = Path.Combine(Options.Output, destination);
        if (Options.WhatIf)
        {
            Logger.LogInformation(
                $"WhatIf moving {file.FullName} to {_outputDirector.GetOutputDirectoryForFile(file, destination)}");
        }
        else
        {
            switch (Options.Mode)
            {
                case Modes.Move:
                    _outputDirector.HandleFile(file, outputDirectory);
                    break;
                case Modes.Copy:
                    _outputDirector.HandleFile(file, outputDirectory, Modes.Copy);
                    break;
            }
        }
    }
    
}