using System.Security.Cryptography;
using System.Text;

namespace api.Services.Account
{
    public class PasswordService : IPasswordService
    {
        const int keySize = 64;
        const int iterations = 10;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA512,
                keySize
            );
            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}