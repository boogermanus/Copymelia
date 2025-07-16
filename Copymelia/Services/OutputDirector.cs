using Copymelia.Core.Constants;
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
    
    public void HandleFile(FileInfo file, string destination, string mode = "move")
    {
        var moved = false;
        var newPath = Path.Combine(GetOutputDirectoryForFile(file, destination), file.Name);
        var count = 1;
        
        while (!moved)
        {
            try
            {
                if (mode == Modes.Move)
                    File.Move(file.FullName, newPath);
                else
                    File.Copy(file.FullName, newPath);

                moved = true;
                _logger.LogInformation($"Handled {mode} for {file.FullName} to {newPath}");
            }
            catch (IOException)
            {
                newPath = Path.Combine(GetOutputDirectoryForFile(file, destination),
                    $"{Path.GetFileNameWithoutExtension(file.FullName)}_{count++}{file.Extension}");
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}{Environment.NewLine}{e.StackTrace}");
            }
        }
    }

    public string GetOutputDirectoryForFile(FileInfo info, string path)
    {
        var year = info.CreationTime.Year.ToString();

        var yearPath = Path.Combine(path, year);
        
        if (!Directory.Exists(yearPath)) Directory.CreateDirectory(yearPath);
        
        return yearPath;
    }
}