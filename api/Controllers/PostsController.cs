using System.Net;
using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _service;

        public PostsController(PostsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _service.GetAllPostsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _service.GetPost(id);
            if (post is null)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = "Post with given id does not exist",
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Post not found",
                    Type = "Post not found",
                });
            }
            return Ok(post);
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