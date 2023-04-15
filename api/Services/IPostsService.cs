using api.Dtos;
using api.Models;

namespace api.Services
{
    public interface IPostsService
    {
        Task<Post> AddPost(AddPostDto addPostDto);
        Task DeletePost(int id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPost(int id);
    }
}