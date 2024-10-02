using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.CustomSettings;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Service.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;
    public TokenService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSettings = jwtSettings;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateAccessToken(ApplicationUser user, string deviceId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var userToken = await _unitOfWork.ApplicationUserTokenRepository.GetUserAsync(p => p.UserId.Equals(user.Id) && p.LoginProvider == deviceId);
        
        //inputString is a string is the combination of toUpper(userId) + loginProvider + value(refreshToken)
        var inputString = StringConverterHelper.CapitalizeString(userToken.UserId.ToString()) + userToken.LoginProvider + userToken.Value;
        //hash the inputString with secrets
        string hashJwtToken = HashJwtStringToken(inputString);
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {   
            new Claim("UserId", user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim("UserEmail", user.Email),
            new Claim("UserPhone", user.PhoneNumber),
            new Claim("TokenHash", hashJwtToken),
            new Claim(ClaimTypes.Role, string.Join(",", userRoles))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = CoreHelper.SystemTimeNow.DateTime.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    public async Task<string> GenerateRefreshToken(ApplicationUser user, string deviceId)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); // Random 64-byte token

        // Check if the user already has a token, retrieve it if it exists
        ApplicationUserToken? userToken = await _unitOfWork.ApplicationUserTokenRepository.GetUserAsync(p => p.UserId.Equals(user.Id) && p.LoginProvider == deviceId);
        string expiryTime = TimeStampHelper.GenerateCustomUnixTimeStamp(_jwtSettings.RefreshTokenExpirationHours, 0, 0).ToString();
    
        // If no token exists for the user/device, create a new one
        if (userToken == null)
        {
            userToken = new ApplicationUserToken
            {
                UserId = user.Id,
                LoginProvider = deviceId,
                Name = "RefreshToken",
                Value = refreshToken,
                ExpiryTime = expiryTime,
            };
        
            // Add the new token to the repository
            await _unitOfWork.ApplicationUserTokenRepository.InsertAsync(userToken);
        }
        else
        {
            // Update the existing token with the new refresh token and expiry time
            userToken.Value = refreshToken;
            userToken.ExpiryTime = expiryTime;
        
            _unitOfWork.ApplicationUserTokenRepository.Update(userToken);
        }

        // Save changes to the database
        await _unitOfWork.SaveChangesAsync();

        return refreshToken;
    }
    public async Task<TokenResponseModel> GenerateTokens(ApplicationUser user, string deviceId)
    {
        var refreshToken = await GenerateRefreshToken(user, deviceId);
        var accessToken = await GenerateAccessToken(user, deviceId);
        return new TokenResponseModel
        {
            accessToken = accessToken,
            refreshToken = refreshToken
        };
    }

    public bool ValidateTokenExpired(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(token))
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Invalid token");
        }
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var expritationClaim = jwtToken.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Exp);
        if(expritationClaim == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Token does not contain an expiration claim");
        }

        var expritationTimeStamp = long.Parse(expritationClaim.Value);
        
        if(String.CompareOrdinal(expritationTimeStamp.ToString(), TimeStampHelper.GenerateUnixTimeStamp()) <= 0)
        {
            return false;
        }
        return true;
    }

    public string HashJwtStringToken(string inputString)
    {
        var tokenString = _jwtSettings.Key + _jwtSettings.Issuer + _jwtSettings.Audience + inputString;
        // Hash the token string with hmacsha256, using the secret key
        var tokenHasher = HmacSHA256Hasher.Hash(tokenString);
        return tokenHasher;
    }

    public bool ValidTokenHash(string token, string inputString)
    {
        string tokenHash = HashJwtStringToken(inputString);
        return tokenHash == token;
    }
}