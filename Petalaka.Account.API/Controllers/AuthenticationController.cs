using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IAccountService _accountService;
    public AuthenticationController
        (
            IAccountService accountService
        )
    {
        _accountService = accountService;
    }
    /// <summary>
    /// User register account
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
        await _accountService.RegisterAccount(request);
        return new BaseResponse(StatusCodes.Status201Created, "Created successfully");
    }
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/authentication")]
    public async Task<BaseResponse<LoginResponseModel>> Login([FromBody] LoginRequestModel request)
    {
        var loginResult = await _accountService.Login(request);
        return new BaseResponse<LoginResponseModel>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = loginResult,
            Message = "Login success"
        };
    }

}