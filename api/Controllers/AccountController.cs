using System.IdentityModel.Tokens.Jwt;
using api.Dtos.Account;
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
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _accountService.Login(userLoginDto);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                };
                Response.Cookies.Append(_config["AuthOptions:AuthCookieName"], token, cookieOptions);
                return Ok(token);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return Ok();
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId);
            return Ok();
        }
    }
}