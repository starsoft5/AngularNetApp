using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebApplication1.Helper
{
public class PasswordHasher
{
    // Hash a password with a unique salt
    public static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a secure PRNG
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password using PBKDF2
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        // Combine salt and hash for storage
        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    // Verify a password against a stored hash
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Extract the salt from the stored hash
        string[] parts = hashedPassword.Split('.');
        byte[] salt = Convert.FromBase64String(parts[0]);
        string storedHash = parts[1];

        // Hash the provided password with the stored salt
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        // Compare the hashes
        return hash == storedHash;
    }
}
}