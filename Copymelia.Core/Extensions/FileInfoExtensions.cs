using Copymelia.Core.Constants;

namespace Copymelia.Core.Extensions;

public static class FileInfoExtensions
{
    public static bool IsImage(this FileInfo file)
    {
        return FileExtensions.ImageExtensions.Contains(file.Extension);
    }    
    
    public static bool IsDocument(this FileInfo file)
    {
        return FileExtensions.DocumentExtensions.Contains(file.Extension.ToLower());
    }

    public static bool IsVideo(this FileInfo file)
    {
        return FileExtensions.VideoExtensions.Contains(file.Extension.ToLower());
    }

    public static bool IsAudio(this FileInfo file)
    {
        return FileExtensions.AudioExtension == file.Extension.ToLower();
    }
    
    public static bool IsZip(this FileInfo file)
    {
        return FileExtensions.ZipExtension == file.Extension.ToLower();
    }
    
    
}