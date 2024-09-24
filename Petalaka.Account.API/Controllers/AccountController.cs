using Microsoft.AspNetCore.Authorization;
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
      
    [HttpPost]
    [Route("v1/email/confirmation")]
    public async Task<BaseResponse> ConfirmEmail([FromBody] ConfirmEmailRequestModel request)
    {
        await _accountService.ConfirmEmail(request);
        return new BaseResponse(StatusCodes.Status200OK, "Email confirmed");
    }
    
    [HttpPost]
    [Route("v1/email/otp")]
    public async Task<BaseResponse> SendEmailOtp([FromBody] ResendEmailConfirmationRequestModel request)
    {
        await _accountService.SendEmailOtp(request);
        return new BaseResponse(StatusCodes.Status200OK, "Email OTP sent");
    }
}