using api.Dtos;
using api.Dtos.Post;
using api.Models;

namespace api.Repositories.PostsRepository
{
    public interface IPostsRepository
    {
        Task<IEnumerable<PostResponseDto>> GetAllPosts();
        Task<PostResponseDto?> GetPost(int id);
        Task<Post> AddPost(AddPostDto addPostDto);
        Task DeletePost(int id);
    }
}