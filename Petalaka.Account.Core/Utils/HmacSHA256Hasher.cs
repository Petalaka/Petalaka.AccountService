using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Petalaka.Account.Core.ExceptionCustom;

namespace Petalaka.Account.Core.Utils;

public static class HmacSHA256Hasher
{
    public static string Hash(string input)
    {
        
        string? secret = Environment.GetEnvironmentVariable("SecretKeyHmacSHA256:SecretKey");
        if (string.IsNullOrWhiteSpace(secret))
        {
            var configuration = ReadConfiguration.ReadBasePathAppSettings();
            secret = configuration["SecretKeyHmacSHA256:SecretKey"];
        }

        if (string.IsNullOrEmpty(secret))
        {
            throw new CoreException(StatusCodes.Status500InternalServerError, "Secret cannot be null or empty");

        }

        if (string.IsNullOrEmpty(input))
        {
            throw new CoreException(StatusCodes.Status500InternalServerError, "Input cannot be null or empty");
        }

        // Convert input to byte array
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Create an HMACSHA256 instance with the key
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        // Compute the hash
        byte[] hashBytes = hmac.ComputeHash(inputBytes);

        // Convert the hash to a hexadecimal string
        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2")); // Convert each byte to hex
        }
        return sb.ToString();
    }
}