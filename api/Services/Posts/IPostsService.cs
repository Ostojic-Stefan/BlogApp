using api.Dtos;
using api.Dtos.Post;
using api.Models;

namespace api.Services.Posts
{
    public interface IPostsService
    {
        Task<Post> AddPost(AddPostDto addPostDto);
        Task DeletePost(int id);
        Task<ServiceResponse<IEnumerable<PostResponseDto>>> GetAllPosts();
        Task<ServiceResponse<PostResponseDto>> GetPost(int id);
    }
}