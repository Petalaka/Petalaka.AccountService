using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.CustomSettings;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Service.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    public TokenService(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> GenerateAccessToken(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
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
    
    public async Task<string> GenerateRefreshToken(ApplicationUser user)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); // Random 64-byte token

        // Store refresh token in the database for the user
        await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);

        return refreshToken;
    }
    public async Task<TokenResponseModel> GenerateTokens(ApplicationUser user)
    {
        var accessToken = await GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshToken(user);

        return new TokenResponseModel
        {
            accessToken = accessToken,
            refreshToken = refreshToken
        };
    }
}