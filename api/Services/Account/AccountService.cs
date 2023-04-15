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
        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            // Check if user exists in the db
            var user = await _unitOfWork.UsersRepository.GetUser(userLoginDto);

            if (user is null)
                throw new Exception("User does not exist");

            // Check email and password
            // REDUNDANT!!!!!
            if (user.Email != userLoginDto.Email)
                throw new Exception("Bad Credentials");

            bool isValid = _passwordService.VerifyPassword(userLoginDto.Password, user.HashedPassword, user.Salt);

            if (!isValid)
                throw new Exception("Bad Credentials");

            var token = _tokenService.GenerateToken(user);
            return token;
        }

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            // Check if user with the same email or username exists
            var user = await _unitOfWork.UsersRepository.GetUsersByEmailAndName(userRegisterDto);

            // If the user with same credentials already exists, what to do?
            if (user is not null)
                throw new Exception("User email or username already exists");

            // if its the unique user, then create a new user entity and save it to the db
            await _unitOfWork.UsersRepository.AddUser(userRegisterDto);
            _unitOfWork.CommitTransaction();
        }

        public async Task Logout()
        {
            // Delete the cookie
        }
    }
}