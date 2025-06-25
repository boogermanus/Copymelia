using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public abstract class FileProcessorBase<T> where T : class
{
    protected ILogger<T> Logger { get; }
    
    protected FileProcessorBase(ILogger<T> logger)
    {
        Logger = logger;
    }
    public void Process(string path)
    {
        var directories = Directory.EnumerateDirectories(path);
        var files = Directory.EnumerateFiles(path);
        ProcessDirectories(directories);
        ProcessFiles(files);
    }
    
    private void ProcessDirectories(IEnumerable<string> directories)
    {
        foreach (var directory in directories)
        {
            var subDirectories = Directory.GetDirectories(directory);
            ProcessDirectories(subDirectories);
            ProcessFiles(Directory.GetFiles(directory));
        }
    }
    
    private void ProcessFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            ProcessFile(file);
        }
    }
    
    protected abstract void ProcessFile(string filePath);
}