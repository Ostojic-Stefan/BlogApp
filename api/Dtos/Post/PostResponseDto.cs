namespace api.Dtos.Post
{
    public class PostResponseDto
    {
        public required string Title { get; set; }
        public required string Body { get; set; }
        public string Username { get; set; }
    }
}