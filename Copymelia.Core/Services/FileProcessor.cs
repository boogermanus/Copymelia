using MetadataExtractor;
using Microsoft.Extensions.Logging;
using Directory = System.IO.Directory;

namespace Copymelia.Core.Services;

public class FileProcessor : FileProcessorBase<FileProcessor>
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

    public FileProcessor(ILogger<FileProcessor> logger) : base(logger)
    {
        _logger = logger;
    }

    protected override void ProcessFile(string filePath)
    {
        throw new NotImplementedException();
    }
}