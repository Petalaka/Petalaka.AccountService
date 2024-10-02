using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.Interface;

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
        await _next(context); // Call the next middleware

        // Check if the response status is 401 Unauthorized
        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}