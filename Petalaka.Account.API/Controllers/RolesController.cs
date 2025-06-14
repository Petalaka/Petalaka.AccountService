﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.RoleRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.RoleResponse;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class RolesController : BaseController
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    /// <summary>
    /// Create new role
    /// </summary>
    /// <remarks>
    /// Require authentication as admin role to perform this function
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/role")]
    [Authorize(Roles="ADMIN")]
    public async Task<ActionResult<BaseResponse>> CreateRole([FromBody] CreateRoleRequestModel request)
    {
        await _roleService.CreateRoleAsync(request);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "Created successfully"));
    }
    
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <remarks>
    /// Require authentication as admin role to perform this function
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/roles")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<BaseResponse<IEnumerable<GetRoleResponseModel>>>> GetRoles()
    {
        var getRolesResult = await _roleService.GetRolesAsync();
        return Ok(new BaseResponse<IEnumerable<GetRoleResponseModel>>
        {
            StatusCode = StatusCodes.Status200OK,
            Data = getRolesResult,
            Message = "Get roles success"
        });
    }
}