using api.Context.UnitOfWork;
using api.Dtos.Account;
using api.Services.Token;

namespace api.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _config;

        public AccountService(
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IPasswordService passwordService,
            IHttpContextAccessor accessor,
            IConfiguration config
            )
        {
            _accessor = accessor;
            _config = config;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto userLoginDto)
        {
            var user = await _unitOfWork.UsersRepository.GetUser(userLoginDto);
            var response = new ServiceResponse<string>();
            if (user is null)
            {
                response.Success = false;
                response.Message = "User does not exist";
                return response;
            }
            bool isValid = _passwordService.VerifyPassword(userLoginDto.Password, user.HashedPassword, user.Salt);
            if (!isValid)
            {
                response.Success = false;
                response.Message = "Bad Credentials";
                return response;
            }
            var token = _tokenService.GenerateToken(user);
            response.Data = token;
            return response;
        }

        public async Task<ServiceResponse<string>> Register(UserRegisterDto userRegisterDto)
        {
            var user = await _unitOfWork.UsersRepository.GetUsersByEmailAndName(userRegisterDto);
            var response = new ServiceResponse<string>();
            if (user is not null)
            {
                response.Success = false;
                response.Message = "User email or username already exists";
                return response;
            }
            await _unitOfWork.UsersRepository.AddUser(userRegisterDto);
            _unitOfWork.CommitTransaction();
            return response;
        }

        public async Task Logout()
        {
            _accessor.HttpContext.Response.Cookies.Delete(_config["AuthOptions:AuthCookieName"]);
        }

        public async Task<ServiceResponse<UserResponseDto>> GetAccountById(int id)
        {
            var user = await _unitOfWork.UsersRepository.GetUsersById(id);
            var response = new ServiceResponse<UserResponseDto>();
            if (user is null)
            {
                response.Success = false;
                response.Message = "User Not Found";
                return response;
            }
            response.Data = new UserResponseDto
            {
                Email = user.Email,
                Username = user.Username
            };
            return response;
        }
    }
}