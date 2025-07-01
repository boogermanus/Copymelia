using Microsoft.Extensions.Logging;

namespace Copymelia.Core.Services;

public class FileProcessor : FileProcessorBase<FileProcessor>
{
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

    private readonly IEnumerable<string> _videoExtensions = [];
    public FileProcessor(ILogger<FileProcessor> logger) : base(logger) { }

    protected override void ProcessFile(string filePath)
    {
        throw new NotImplementedException();
    }
}