using System.Security.Cryptography;
using System.Text;

namespace Petalaka.Account.Core.Utils;

public class PasswordHasher
{
    // Generate a random salt
    public static string GenerateSalt(int size = 32)
    {
        using var rng = new RNGCryptoServiceProvider();
        var saltBytes = new byte[size];
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    // Hash the password with the salt using SHA-256
    public static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        // Combine the password and salt
        var saltedPassword = string.Concat(password, salt);

        // Convert the combined password and salt to a byte array
        var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

        // Compute the hash
        var hashBytes = sha256.ComputeHash(saltedPasswordBytes);

        // Convert the hash to a Base64 string
        return Convert.ToBase64String(hashBytes);
    }

    // Verify if the provided password matches the hashed password
    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        var hashOfEnteredPassword = HashPassword(enteredPassword, storedSalt);
        return hashOfEnteredPassword.Equals(storedHash);
    }
}