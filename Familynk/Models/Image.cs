namespace Familynk.Models;

public class Image
{
    public int  ImageId { get; set; }
    public string FileName { get; set; } = default!;
    public string FileExtension { get; set; } = default!;
    public int? width { get; set; }
    public byte[]? Bytes { get; set; }
}

