using api.Models;

namespace api.Dtos
{
    public class AddPostDto
    {
        public IFormFile Image { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}