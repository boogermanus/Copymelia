using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor
{
    private readonly IEnumerable<string> _imageExtensions = [".jpg", ".jpeg", ".png"];
    private readonly ILogger<FileProcessor> _logger;
    private Options _options;
    private int _files = 0;
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
        ".ppt",
        ".txt"
    ];

    private readonly IEnumerable<string> _videoExtensions = [
        ".mp4",
        ".mov",
        ".avi",
        ".wmv",
        ".mpeg"
    ];

    public FileProcessor(ILogger<FileProcessor> logger)
    {
        _logger = logger;
    }
    
    public void Process(Options options)
    {
        _options = options;
        _logger.LogInformation($"Processing path '{options.Path}'");
        var directories = Directory.EnumerateDirectories(options.Path);
        var files = Directory.EnumerateFiles(options.Path);
        ProcessDirectories(directories);
        ProcessFiles(files);
        _logger.LogInformation($"Files processed: {_files}");
    }
    
    private void ProcessDirectories(IEnumerable<string> directories)
    {
        foreach (var directory in directories)
        {
            _logger.LogInformation($"Processing directory '{directory}'");
            var subDirectories = Directory.GetDirectories(directory);
            ProcessDirectories(subDirectories);
            ProcessFiles(Directory.GetFiles(directory));
        }
    }
    
    private void ProcessFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            _logger.LogInformation($"Processing file '{file}'");
            ProcessFile(file);
        }
    }

    private void ProcessFile(string filePath)
    {
        _logger.LogInformation($"Processing file '{filePath}'");
        _files++;
    }
}