using api.Dtos.Account;
using api.Models;

namespace api.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResponse<string>> Login(UserLoginDto userLoginDto);
        Task<ServiceResponse<string>> Register(UserRegisterDto userRegisterDto);
        Task<ServiceResponse<UserResponseDto>> GetAccountById(int id);
        Task Logout();
    }
}