using Copymelia.Constants;
using Copymelia.Models;
using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class OutputDirector
{
    private readonly ILogger<OutputDirector> _logger;

    public OutputDirector(ILogger<OutputDirector> logger)
    {
        _logger = logger;
    }
    
    public void Build(Options options)
    {
        CreatePathIfNotExists(options.Output, $"Creating output directory '{options.Output}'");
        
        var imagesPath = Path.Combine(options.Output, OutputDirectories.ImagesDirectory);
        CreatePathIfNotExists(imagesPath, $"Creating images directory '{imagesPath}'");
        
        var documentsPath = Path.Combine(options.Output, OutputDirectories.DocumentsDirectory);
        CreatePathIfNotExists(documentsPath, $"Creating output directory '{documentsPath}'");
        
        var videosPath = Path.Combine(options.Output, OutputDirectories.VideosDirectory);
        CreatePathIfNotExists(videosPath, $"Creating documents directory '{videosPath}'");
        
        var audioPath = Path.Combine(options.Output, OutputDirectories.AudioDirectory);
        CreatePathIfNotExists(audioPath, $"Creating audio directory '{audioPath}'");
        
        var zipTempPath = Path.Combine(Path.GetTempPath(), OutputDirectories.ZipTempDirectory);
        CreatePathIfNotExists(zipTempPath, $"Creating zip temp directory '{zipTempPath}'");
    }

    private void CreatePathIfNotExists(string path, string loggerMessage)
    {
        if(Path.Exists(path)) return;
        _logger.LogInformation(loggerMessage);
        Directory.CreateDirectory(path);
    }
    
    public void HandleFile(FileInfo source, string destination, string mode = "move")
    {
        var moved = false;
        var newPath = Path.Combine(destination, source.Name);
        var count = 1;
        while (!moved)
        {
            try
            {
                if (mode == Modes.Move)
                    File.Move(source.FullName, newPath);
                else
                    File.Copy(source.FullName, newPath);
                // File.Copy(source.FullName, newPath);
                moved = true;
                _logger.LogInformation($"Moved {source.FullName} to {newPath}");
            }
            catch (IOException)
            {
                newPath = Path.Combine(destination,
                    $"{Path.GetFileNameWithoutExtension(source.FullName)}_{count++}{source.Extension}");
            }
        }
    }
}