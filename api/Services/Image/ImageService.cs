namespace api.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ImageService> _logger;
        private readonly string _imageSaveDirectory;
        public ImageService(IWebHostEnvironment env, ILogger<ImageService> logger)
        {
            _logger = logger;
            _env = env;

            _imageSaveDirectory = Path.Combine(_env.WebRootPath, "Images");

            if (!Directory.Exists(_imageSaveDirectory))
                Directory.CreateDirectory(_imageSaveDirectory);
        }

        public async Task<ImageResult> SaveImageAsync(ImageData imageData)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + imageData.FileName;
                string savePath = Path.Combine(_imageSaveDirectory, fileName);

                using (var fStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write))
                {
                    await imageData.Stream.CopyToAsync(fStream);
                }

                return new ImageResult
                {
                    SavePath = savePath,
                    WebRequestPath = Path.Combine("Images", fileName)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}