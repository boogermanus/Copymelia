using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{
    private readonly IEnumerable<string> _imageExtensions = [".jpg", ".jpeg", ".png", ".tiff"];
    private readonly IEnumerable<string> _documentExtensions =
    [
        ".pdf",
        ".docx",
        ".doc",
        ".rtf",
        ".xlsx",
        ".xls",
        ".pptx",
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
    private readonly string _musicExtension = ".mp3";

    public FileProcessor(ILogger<FileProcessor> logger): base(logger) {}
    protected override void ProcessFile(string file)
    {
        var info = new FileInfo(file);
        
        if(_imageExtensions.Contains(info.Extension))
            Logger.LogInformation($"Identified image: {info.Name}");
        
        if (_documentExtensions.Contains(info.Extension))
            Logger.LogInformation($"Identified document: {info.Name}");
        
        if (_videoExtensions.Contains(info.Extension))
            Logger.LogInformation($"Identified video: {info.Name}");
        
        if(info.Extension == _musicExtension)
            Logger.LogInformation($"Identified music: {info.Name}");
        
        Logger.LogInformation($"Processed File: '{file}'");
    }
}