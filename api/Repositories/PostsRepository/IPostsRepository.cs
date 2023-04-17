using api.Dtos;
using api.Dtos.Post;
using api.Models;

namespace api.Repositories.PostsRepository
{
    public interface IPostsRepository
    {
        Task<IEnumerable<PostResponseDto>> GetAllPosts();
        Task<IEnumerable<PostResponseDto>> GetAllPosts(int userId);
        Task<PostResponseDto?> GetPost(int id);
        Task<Post> AddPost(Post addPostDto);
        Task DeletePost(int id);
    }
}