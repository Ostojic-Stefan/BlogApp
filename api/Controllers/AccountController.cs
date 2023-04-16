using System.Net;
using api.Dtos.Account;
using api.Extensions;
using api.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        public AccountController(IAccountService accountService, IConfiguration config)
        {
            _config = config;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegiserDto)
        {
            await _accountService.Register(userRegiserDto);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var response = await _accountService.Login(userLoginDto);
            if (!response.Success)
            {
                return BadRequest(new ProblemDetails
                {
                    Detail = response.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Duplicate Entry",
                    Type = "Duplicate Entry"
                });
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };
            Response.Cookies.Append(_config["AuthOptions:AuthCookieName"], response.Data, cookieOptions);
            return NoContent();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return NoContent();
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.GetUserId();
            var user = await _accountService.GetAccountById(userId);
            return Ok(user.Data);
        }
    }
}