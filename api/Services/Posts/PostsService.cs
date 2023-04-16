using api.Context.UnitOfWork;
using api.Dtos;
using api.Dtos.Post;
using api.Models;

namespace api.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostsService> _logger;
        public PostsService(IUnitOfWork unitOfWork, ILogger<PostsService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<PostResponseDto>>> GetAllPosts()
        {
            var posts = await _unitOfWork.PostsRepository.GetAllPosts();
            return new ServiceResponse<IEnumerable<PostResponseDto>>
            {
                Data = posts
            };
        }

        public async Task<ServiceResponse<PostResponseDto>> GetPost(int id)
        {
            var post = await _unitOfWork.PostsRepository.GetPost(id);
            var response = new ServiceResponse<PostResponseDto>();
            if (post is null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            response.Data = post;
            return response;
        }

        public async Task<Post> AddPost(AddPostDto addPostDto)
        {
            var post = await _unitOfWork.PostsRepository.AddPost(addPostDto);
            _unitOfWork.CommitTransaction();
            return post;
        }

        public async Task DeletePost(int id)
        {
            await _unitOfWork.PostsRepository.DeletePost(id);
            _unitOfWork.CommitTransaction();
        }
    }
}