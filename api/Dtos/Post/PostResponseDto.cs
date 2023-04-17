namespace api.Dtos.Post
{
    public class PostResponseDto
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required string ImagePath { get; set; }
        public required string Username { get; set; }
    }
}