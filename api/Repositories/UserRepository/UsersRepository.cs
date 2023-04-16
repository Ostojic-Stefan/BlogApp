using System.Data;
using api.Dtos.Account;
using api.Models;
using api.Services.Account;
using Dapper;

namespace api.Repositories.UserRepository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _connection;
        private readonly IPasswordService _passwordService;

        public UsersRepository(IDbConnection connection, IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _connection = connection;
        }

        public async Task AddUser(UserRegisterDto userRegisterDto)
        {
            var sql = "INSERT INTO users (username, email, hashedPassword, salt) VALUES(@username, @email, @hashedPassword, @salt)";
            var parameters = new DynamicParameters();

            var hashedPassword = _passwordService.HashPassword(userRegisterDto.Password, out byte[] salt);

            parameters.Add("username", userRegisterDto.Username);
            parameters.Add("email", userRegisterDto.Email);
            parameters.Add("hashedPassword", hashedPassword);
            parameters.Add("salt", salt);

            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<User?> GetUser(UserLoginDto userLoginDto)
        {
            var sql = "SELECT * FROM users WHERE email = @email";
            var parameters = new DynamicParameters();
            parameters.Add("email", userLoginDto.Email);
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, parameters);
        }

        public async Task<User?> GetUsersByEmailAndName(UserRegisterDto userRegisterDto)
        {
            var sql = "SELECT username, email FROM users WHERE username = @username AND email = @email";
            var parameters = new DynamicParameters();
            parameters.Add("username", userRegisterDto.Username);
            parameters.Add("email", userRegisterDto.Email);
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, parameters);
        }

        public async Task<User?> GetUsersById(int id)
        {
            var sql = "SELECT username, email FROM users WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, parameters);
        }
    }
}