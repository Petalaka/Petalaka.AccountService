using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.CustomSettings;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.API.Security;

public class CustomJwtBearerEvents : JwtBearerEvents
{
    /*public override async Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        ITokenService tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
        IUnitOfWork unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
        JwtSettings jwtSettings = context.HttpContext.RequestServices.GetRequiredService<JwtSettings>();

        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        string deviceId = context.Request.Headers["User-Agent"].ToString();

        var jwtHandler = new JwtSecurityTokenHandler();

        if (context.Exception is SecurityTokenExpiredException)
        {
            return;
        }

        if (!jwtHandler.CanReadToken(token))
        {
            return;
        }

        var jwtToken = jwtHandler.ReadJwtToken(token);
        // Accessing the payload (claims)
        var payload = jwtToken.Claims.ToList();

        string userId = StringConverterHelper.CapitalizeString(payload.FirstOrDefault(p => p.Type == "UserId")?.Value);
        string tokenHash = payload.FirstOrDefault(p => p.Type == "TokenHash")?.Value;

        var userToken = await unitOfWork.ApplicationUserTokenRepository.GetUserAsync(p =>
            p.UserId.ToString() == userId &&
            p.LoginProvider == deviceId);

        if (userToken == null)
        {
            context.Response.StatusCode = 401; // Unauthorized
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"User not found\"}");
            return;
        }

        // Check if the refresh token is expired yet?
        if (String.CompareOrdinal(userToken.ExpiryTime, TimeStampHelper.GenerateUnixTimeStamp()) < 0)
        {
            context.Fail("RefreshToken expired");
            return;
        }

        // Validate the token hash with the input string
        string inputString = $"{userId}{deviceId}{userToken.Value}";
        if (!tokenService.ValidTokenHash(tokenHash, inputString))
        {
            context.Fail("Invalid token hash");
            return;
        }
        
        // Generate new tokens
        ApplicationUser user = await unitOfWork.ApplicationUserRepository.FindUndeletedAsync(p => p.Id == userToken.UserId);
        var accessToken = await tokenService.GenerateAccessToken(user, deviceId);
        userToken.ExpiryTime = TimeStampHelper.GenerateCustomUnixTimeStamp(jwtSettings.RefreshTokenExpirationHours, 0, 0).ToString();
        userToken.Value = await tokenService.GenerateRefreshToken(user, deviceId);
        await unitOfWork.SaveChangesAsync();
        context.Request.Headers.Remove("Authorization");
        context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
        //pricipal get from read token
        context.Principal = jwtHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        }, out _);
        context.Success();
        return;
    }*/
}
