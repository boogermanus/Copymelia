using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class ImageProcessor
{
    private readonly ILogger<ImageProcessor> _logger;
    private readonly IEnumerable<string> _imageExtensions = [".jpg", ".jpeg", ".png"];
    public List<FileInfo> Images = new();

    public ImageProcessor(ILogger<ImageProcessor> logger)
    {
        _logger = logger;
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
            var info =  new FileInfo(file);
            if (_imageExtensions.Contains(info.Extension))
            {
                Images.Add(info);
                _logger.LogInformation($"Added image {info.FullName}");
            }
        }
    }
}