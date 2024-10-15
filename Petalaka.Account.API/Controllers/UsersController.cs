using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.UserFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.UserSorts;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Provider get their own profile
    /// </summary>
    /// <remarks>
    /// Require authentication as PROVIDER role to perform this function
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/profile")]
    [Authorize(Roles = "USER")]
    public async Task<ActionResult<BaseResponse<GetMyProfileResponse>>> GetMyProfile()
    {
        var userId = User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;
        var result = await _userService.GetMyProfile(userId);
        return Ok(new BaseResponse<GetMyProfileResponse>(StatusCodes.Status200OK, "Get profile successfully", result));
    }
    
    /// <summary>
    /// Provider update their own profile
    /// </summary>
    /// <remarks>
    /// Require authentication as PROVIDER role to perform this function
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("v1/profile")]
    [Authorize(Roles = "USER")]
    public async Task<ActionResult<BaseResponse>> UpdateMyProfile([FromBody] UpdateMyProfileRequest request)
    {
        await _userService.UpdateMyProfile(request);
        return Accepted(new BaseResponse(StatusCodes.Status202Accepted, "Update profile successfully"));
    }
    
    /// <summary>
    /// Admin get all users
    /// </summary>
    /// <remarks>
    /// Require authentication as ADMIN role to perform this function
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/users")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<BaseResponsePagination<GetAllUserResponse>>> GetUsers([FromQuery] RequestOptionsBase<GetAllUserFilterOptions, GetAllUserSortoptions> request)
    {
        var result = await _userService.GetUsers(request);
        var baseResponsePagination = _mapper.Map<BaseResponsePagination<GetAllUserResponse>>(result);
        baseResponsePagination.StatusCode = StatusCodes.Status200OK;
        baseResponsePagination.Message = "Get users successfully";
        return Ok(baseResponsePagination);
    }
}