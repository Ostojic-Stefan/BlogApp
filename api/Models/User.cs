namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public required byte[] Salt { get; set; }
    }
}