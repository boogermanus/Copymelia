using Copymelia.Constants;
using Copymelia.Extensions;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class FileProcessor : FileProcessorBase
{
   

    public FileProcessor(ILogger<FileProcessor> logger): base(logger) {}
    protected override void ProcessFile(string file)
    {
        var info = new FileInfo(file);
        
        if(info.IsImage())
            Logger.LogInformation($"Identified image: {info.Name}");
        
        if (info.IsDocument())
            Logger.LogInformation($"Identified document: {info.Name}");
        
        if (info.IsVideo())
            Logger.LogInformation($"Identified video: {info.Name}");
        
        if(info.IsAudio())
            Logger.LogInformation($"Identified music: {info.Name}");
        
        Logger.LogInformation($"Processed File: '{file}'");
    }
}