using api.Dtos;
using api.Models;

namespace api.Repositories
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPost(int id);
        Task<Post> AddPost(AddPostDto addPostDto);
        Task DeletePost(int id);
    }
}