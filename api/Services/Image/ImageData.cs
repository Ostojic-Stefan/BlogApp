namespace api.Services.Image
{
    public class ImageData
    {
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
}