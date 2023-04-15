using api.Context.UnitOfWork;
using api.Dtos;
using api.Models;

namespace api.Services
{
    public class PostsService : IPostsService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<PostsService> _logger;
        public PostsService(UnitOfWork unitOfWork, ILogger<PostsService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
            => await _unitOfWork.PostsRepository.GetAllPosts();

        public async Task<Post?> GetPost(int id)
           => await _unitOfWork.PostsRepository.GetPost(id);

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