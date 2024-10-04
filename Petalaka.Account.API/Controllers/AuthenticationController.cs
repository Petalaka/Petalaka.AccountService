using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AuthenticationRequest;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenService;
    public AuthenticationController(
            IAuthenticationService authenService
        )
    {
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
        string userAgent = Request.Headers["User-Agent"].ToString();
        var loginResult = await _authenService.Login(request, userAgent);
        return new BaseResponse<LoginResponseModel>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = loginResult,
            Message = "Login success"
        };
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("v1/authentication")]
    [Authorize]
    public async Task<BaseResponse> Logout()
    {
        string userId = User.Claims.FirstOrDefault(p => p.Type == "UserId").Value;
        string deviceId = Request.Headers["User-Agent"].ToString();
        await _authenService.Logout(userId, deviceId);
        return new BaseResponse(StatusCodes.Status200OK, "Logout success");
    }
}