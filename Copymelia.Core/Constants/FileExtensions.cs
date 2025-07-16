namespace Copymelia.Core.Constants;

public static class FileExtensions
{
    public static readonly IEnumerable<string> ImageExtensions = [".jpg", ".jpeg", ".png", ".tiff"];
    public static readonly IEnumerable<string> DocumentExtensions =
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
    public static readonly IEnumerable<string> VideoExtensions = [
        ".mp4",
        ".mov",
        ".avi",
        ".wmv",
        ".mpeg"
    ];
    public static readonly string AudioExtension = ".mp3";
    public static readonly string ZipExtension = ".zip";
}