namespace api.Services.Image
{
    public interface IImageService
    {
        Task<ImageResult> SaveImageAsync(ImageData imageData);
    }
}