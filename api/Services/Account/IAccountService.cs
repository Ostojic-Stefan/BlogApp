using api.Dtos.Account;

namespace api.Services.Account
{
    public interface IAccountService
    {
        Task<string> Login(UserLoginDto userLoginDto);
        Task Logout();
        Task Register(UserRegisterDto userRegisterDto);
    }
}