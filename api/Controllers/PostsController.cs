using System.Net;
using api.Dtos;
using api.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var response = await _service.GetAllPosts();
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var response = await _service.GetPost(id);
            if (!response.Success && response.Message == "Not Found")
            {
                return NotFound(new ProblemDetails
                {
                    Detail = "Post with given id does not exist",
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Post not found",
                    Type = "Post not found",
                });
            }
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto addPostDto)
        {
            var post = await _service.AddPost(addPostDto);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _service.DeletePost(id);
            return NoContent();
        }
    }
}