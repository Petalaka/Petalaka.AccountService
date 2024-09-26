using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
      
    /// <summary>
    /// Confirm email by email otp
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/email/confirmation")]
    public async Task<ActionResult<BaseResponse>> ConfirmEmail([FromBody] ConfirmEmailRequestModel request)
    {
        await _accountService.ConfirmEmail(request);
        return Accepted(String.Empty, new BaseResponse(StatusCodes.Status202Accepted, "Email confirmed"));
    }
    
    /// <summary>
    /// Send email otp to user email
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/email/otp")]
    public async Task<ActionResult<BaseResponse>> SendEmailOtp([FromBody] ResendEmailConfirmationRequestModel request)
    {
        await _accountService.SendEmailOtp(request);
        return Accepted(String.Empty, new BaseResponse(StatusCodes.Status202Accepted, "Email OTP sent"));
    }
    
    /// <summary>
    /// Change password
    /// </summary>
    /// <remarks>
    /// Require authentication to take email from Bearer token
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("v1/password")]
    [Authorize]
    public async Task<ActionResult<BaseResponse>> ChangePassword([FromBody] ChangePasswordRequestModel request)
    {
        var userEmail = User.Claims.FirstOrDefault(p => p.Type == "UserEmail").Value;
        await _accountService.ChangePassword(userEmail, request);
        return Accepted(String.Empty, new BaseResponse(StatusCodes.Status202Accepted, "Password changed"));
    }
    
    /// <summary>
    /// Confirm forgot password, using email otp to verify user (Unsafe method)
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// AccessToken and RefreshToken
    /// </returns>
    [HttpPut]
    [Route("v1/password/recovery")]
    public async Task<ActionResult<BaseResponse>> ForgotPassword([FromBody] ForgotPasswordRequestModel request)
    {
        var response = await _accountService.ForgotPassword(request);
        return Accepted(String.Empty, new BaseResponse(StatusCodes.Status202Accepted, "Forgot password accepted", response));
    }
    
    /// <summary>
    /// New password forgot, using email otp to verify user and take bearer token to change password (Unsafe method)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/password/recovery")]
    [Authorize]
    public async Task<ActionResult<BaseResponse>> NewPasswordForgot([FromBody] NewPasswordRequestModel request)
    {
        string email = User.Claims.FirstOrDefault(p => p.Type == "UserEmail").Value;
        await _accountService.NewPasswordForgot(email, request);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "New password accepted"));
    }
    
    /// <summary>
    /// Sending email reset password token to user email (as redirect link) (Safe method)
    /// </summary>
    /// <param name="request"></param>
    [HttpPut]
    [Route("v2/password/recovery")]
    public async Task<ActionResult<BaseResponse>> ForgotPasswordV2([FromBody] ForgotPasswordV2RequestModel request)
    {
        await _accountService.ForgotPasswordV2(request);
        return Accepted(String.Empty, new BaseResponse(StatusCodes.Status202Accepted, "Forgot password accepted"));
    }
    
    /// <summary>
    /// Create new password when forgot password (Safe method)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v2/password/recovery")]
    public async Task<ActionResult<BaseResponse>> NewPasswordForgotV2([FromBody] NewPasswordForgotV2RequestModel request)
    {
        await _accountService.NewPasswordForgotV2(request);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "New password created"));
    }
}