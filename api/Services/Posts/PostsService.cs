using api.Context.UnitOfWork;
using api.Dtos;
using api.Dtos.Post;
using api.Models;
using api.Services.Image;

namespace api.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostsService> _logger;
        private readonly IImageService _imageService;
        public PostsService(IUnitOfWork unitOfWork, IImageService imageService, ILogger<PostsService> logger)
        {
            _imageService = imageService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<PostResponseDto>>> GetAllPosts(int userId)
        {
            var posts = userId switch
            {
                0 => await _unitOfWork.PostsRepository.GetAllPosts(),
                _ => await _unitOfWork.PostsRepository.GetAllPosts(userId)
            };

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

        public async Task<Post> AddPost(int currentUserId, AddPostDto addPostDto)
        {
            var imageData = new ImageData
            {
                FileName = addPostDto.Image.FileName,
                ContentType = addPostDto.Image.ContentType,
                Stream = addPostDto.Image.OpenReadStream()
            };

            ImageResult result = await _imageService.SaveImageAsync(imageData);

            var post = new Post
            {
                Title = addPostDto.Title,
                Body = addPostDto.Body,
                ImageUrl = result.WebRequestPath,
                UserId = currentUserId
            };

            var createdPost = await _unitOfWork.PostsRepository.AddPost(post);

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