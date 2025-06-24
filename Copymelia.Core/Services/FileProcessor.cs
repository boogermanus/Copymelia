using MetadataExtractor;
using Microsoft.Extensions.Logging;
using Directory = System.IO.Directory;

namespace Copymelia.Core.Services;

public class FileProcessor
{
    private readonly ILogger<FileProcessor> _logger;
    private readonly IEnumerable<string> _imageExtensions = [".jpg", ".jpeg", ".png"];

    private readonly IEnumerable<string> _documentExtensions =
    [
        ".pdf",
        ".docx",
        ".docm",
        ".dotx",
        ".doc",
        ".rtf",
        ".xlsx",
        ".xlsb",
        ".xlsm",
        ".xltx",
        ".xls",
        ".pptx",
        ".potx",
        ".ppt"
    ];

    private readonly List<FileInfo> _images = new();
    private readonly List<FileInfo> _documents = new();

    public FileProcessor(ILogger<FileProcessor> logger)
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
                _images.Add(info);
                // var metaDir = ImageMetadataReader.ReadMetadata(file);
                // foreach (var metaFile in metaDir)
                //     foreach(var tag in metaFile.Tags)
                //         _logger.LogDebug($"{tag.Name} - {tag.Name} = {tag.Description}");
                _logger.LogInformation($"Added image {info.FullName}");
            }

            if (_documentExtensions.Contains(info.Extension))
            {
                _documents.Add(info);
                _logger.LogInformation($"Added document {info.FullName}");
            }
        }
    }
}