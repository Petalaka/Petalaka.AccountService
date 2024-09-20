using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpPost]
    [Route("v1/roles")]
    public async Task<BaseResponse> CreateRole([FromBody] CreateRoleRequestModel request)
    {
        await _roleService.CreateRoleAsync(request);
        return new BaseResponse(StatusCodes.Status201Created, "Created successfully");
    }
    
    [HttpGet]
    [Route("v1/roles")]
    [Authorize(Roles = "user")]
    public async Task<BaseResponse<IEnumerable<GetRoleResponseModel>>> GetRoles()
    {
        var getRolesResult = await _roleService.GetRolesAsync();
        return new BaseResponse<IEnumerable<GetRoleResponseModel>>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = getRolesResult,
            Message = "Get roles success"
        };
    }
}