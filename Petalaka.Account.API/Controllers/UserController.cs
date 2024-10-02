using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Route("v1/profile")]
    [Authorize(Roles = "USER")]
    public async Task<ActionResult<BaseResponse<GetMyProfileResponse>>> GetMyProfile()
    {
        var userId = User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;
        var result = await _userService.GetMyProfile(userId);
        return Ok(new BaseResponse<GetMyProfileResponse>(StatusCodes.Status200OK, "Get profile successfully", result));
    }
    
    [HttpPut]
    [Route("v1/profile")]
    [Authorize(Roles = "USER")]
    public async Task<ActionResult<BaseResponse>> UpdateMyProfile([FromBody] UpdateMyProfileRequest request)
    {
        await _userService.UpdateMyProfile(request);
        return Accepted(new BaseResponse(StatusCodes.Status202Accepted, "Update profile successfully"));
    }
}