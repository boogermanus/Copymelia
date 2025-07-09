using Microsoft.Extensions.Logging;

namespace Copymelia.Services;

public class MoveDirector
{
    private readonly ILogger<MoveDirector> _logger;
    
    public MoveDirector(ILogger<MoveDirector> logger)
    {
        _logger = logger;
    }
    public void Move(FileInfo source, string destination)
    {
        var moved = false;
        var newPath = Path.Combine(destination, source.Name);
        var count = 1;
        while (!moved)
        {
            try
            {
                // File.Move(source.FullName, newPath);
                File.Copy(source.FullName, newPath);
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