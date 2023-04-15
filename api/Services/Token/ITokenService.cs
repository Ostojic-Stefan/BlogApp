using api.Models;

namespace api.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}