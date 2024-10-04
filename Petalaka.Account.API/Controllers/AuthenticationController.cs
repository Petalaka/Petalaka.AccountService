using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;
using System.Security.Claims;
using Azure.Core;

namespace Petalaka.Account.API.Controllers;

public class AuthenticationController : BaseController
{
    private readonly  IAuthenService _authenService;
    private readonly ILogger<AuthenticationController> _logger;
    public AuthenticationController(
        ILogger<AuthenticationController> logger,
            IAuthenService authenService
        )
    {
        _logger = logger;
        _authenService = authenService;
    }
    /// <summary>
    /// User register account with email and password
    /// </summary>
    /// <remarks>
    /// Default role is USER
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/registration")]
    public async Task<BaseResponse> RegisterAccount([FromBody] RegisterRequestModel request)
    {
        await _authenService.RegisterAccount(request);
        return new BaseResponse(StatusCodes.Status201Created, "Created successfully");
    }
    
    /// <summary>
    /// Login By Email and Password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/authentication")]
    public async Task<BaseResponse<LoginResponseModel>> Login([FromBody] LoginRequestModel request)
    {
        var loginResult = await _authenService.Login(request);
        return new BaseResponse<LoginResponseModel>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = loginResult,
            Message = "Login success"
        };
    }

    /// <summary>
    /// Login By Email and Password
    /// </summary>
    [HttpGet("signin-google")]
    public IActionResult SignInWithGoogle()
    {
        var redirectUrl = Url.Action(nameof(HandleGoogleLoginCallback));
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google/callback")]
    public async Task<IActionResult> HandleGoogleLoginCallback()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            return Redirect("https://petalaka-staging.nodfeather.win/400");
        }
        var googleId = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var email = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var loginResult = await _authenService.LoginWithGoogle(googleId, email);
        HttpContext.Response.Cookies.Append("AccessToken", loginResult.AccessToken, new CookieOptions
        {
            HttpOnly = true, // Đảm bảo cookie không thể truy cập bằng JavaScript
            Secure = true,   // Chỉ gửi cookie qua HTTPS
            SameSite = SameSiteMode.Strict, // Chỉ gửi cookie cùng site
            Expires = DateTimeOffset.UtcNow.AddHours(1) // Thời hạn hết hạn
        });

        HttpContext.Response.Cookies.Append("RefreshToken", loginResult.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7) // Refresh token có thời hạn dài hơn
        });
        return Redirect("https://petalaka-staging.nodfeather.win/");
    }

}