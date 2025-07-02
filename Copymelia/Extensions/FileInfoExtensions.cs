using Copymelia.Constants;

namespace Copymelia.Extensions;

public static class FileInfoExtensions
{
    public static bool IsImage(this FileInfo file)
    {
        return FileExtensions.ImageExtensions.Contains(file.Extension);
    }    
    
    public static bool IsDocument(this FileInfo file)
    {
        return FileExtensions.DocumentExtensions.Contains(file.Extension);
    }

    public static bool IsVideo(this FileInfo file)
    {
        return FileExtensions.VideoExtensions.Contains(file.Extension);
    }

    public static bool IsAudio(this FileInfo file)
    {
        return FileExtensions.AudioExtension == file.Extension;
    }
}