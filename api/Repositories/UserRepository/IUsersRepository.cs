using api.Dtos.Account;
using api.Models;

namespace api.Repositories.UserRepository
{
    public interface IUsersRepository
    {
        Task<User?> GetUsersByEmailAndName(UserRegisterDto userRegisterDto);
        Task<User?> GetUsersById(int id);
        Task AddUser(UserRegisterDto userRegisterDto);
        Task<User?> GetUser(UserLoginDto userLoginDto);
    }
}