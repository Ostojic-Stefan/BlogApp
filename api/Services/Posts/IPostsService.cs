using api.Dtos;
using api.Dtos.Post;
using api.Models;

namespace api.Services.Posts
{
    public interface IPostsService
    {
        Task<Post> AddPost(int currentUserId, AddPostDto addPostDto);
        Task DeletePost(int id);
        Task<ServiceResponse<IEnumerable<PostResponseDto>>> GetAllPosts(int userId);
        Task<ServiceResponse<PostResponseDto>> GetPost(int id);
    }
}