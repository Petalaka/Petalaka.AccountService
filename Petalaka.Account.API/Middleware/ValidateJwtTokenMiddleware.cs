using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.API.Middleware;

public class ValidateJwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    public ValidateJwtTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        IUnitOfWork _unitOfWork = context.RequestServices.GetRequiredService<IUnitOfWork>();
        ITokenService tokenService = context.RequestServices.GetRequiredService<ITokenService>();
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
        
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var payload = jwtToken.Claims.ToList();
        
            string userId = StringConverterHelper.CapitalizeString(payload.FirstOrDefault(p => p.Type == "UserId")?.Value);
            string tokenHash = payload.FirstOrDefault(p => p.Type == "TokenHash")?.Value;
            string deviceId = context.Request.Headers["User-Agent"].ToString();
        
            var userToken = await _unitOfWork.ApplicationUserTokenRepository.GetUserAsync(p =>
                p.UserId.ToString() == userId &&
                p.LoginProvider == deviceId);
            if(userToken == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            
            if (String.CompareOrdinal(userToken.ExpiryTime, TimeStampHelper.GenerateUnixTimeStamp()) < 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Validate the token hash with the input string
            string inputString = $"{userId}{deviceId}{userToken.Value}";
            if (!tokenService.ValidTokenHash(tokenHash, inputString))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
        }
        await _next(context); // Call the next middleware

    }
}