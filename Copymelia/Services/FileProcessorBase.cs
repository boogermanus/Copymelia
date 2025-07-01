using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public abstract class FileProcessorBase
{
    protected ILogger Logger { get; }
    protected Options Options { get; set; }
    protected int Files { get; set; } = 0;

    public FileProcessorBase(ILogger logger)
    {
        Logger = logger;
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
    
    private void ProcessDirectories(IEnumerable<string> directories)
    {
        foreach (var directory in directories)
        {
            Logger.LogInformation($"Processing directory '{directory}'");
            var subDirectories = Directory.GetDirectories(directory);
            ProcessDirectories(subDirectories);
            ProcessFiles(Directory.GetFiles(directory));
        }
    }
    
    private void ProcessFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            Logger.LogInformation($"Processing file '{file}'");
            Files++;
            ProcessFile(file);
        }
    }
    
    protected abstract void ProcessFile(string file);
}