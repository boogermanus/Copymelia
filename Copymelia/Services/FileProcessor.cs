using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{

    private readonly ILogger<FileProcessor> _logger;
    private Options _options;
    private int _files = 0;
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

    public FileProcessor(ILogger<FileProcessor> logger): base(logger) {}
    protected override void ProcessFile(string file)
    {
        Logger.LogInformation($"Processed File: {file}");
    }
}