using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.ProviderResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.ProviderFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.ProviderSorts;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class ProvidersController : BaseController
{
    private readonly IProviderService _providerService;
    private readonly IMapper _mapper;
    public ProvidersController(IProviderService providerService, IMapper mapper)
    {
        _providerService = providerService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Create Account for Provider
    /// </summary>
    /// <remarks>
    /// Require authentication as ADMIN role to perform this function
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/provider")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<BaseResponse>> CreateProviderAsync([FromBody] CreateProviderRequest request)
    {
        await _providerService.CreateProviderAsync(request);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "Provider created successfully"));
    }
    
    /// <summary>
    /// Get All Providers including their Accounts
    /// </summary>
    /// <remarks>
    /// Require authentication as ADMIN role to perform this function
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/providers")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<BaseResponsePagination<GetAllProviderResponse>>>
        GetProvidersAsync([FromQuery] RequestOptionsBase<GetAllProvidersFilterOptions, GetAllProviderSortOptions> request)
    {
        var result = await _providerService.GetProvidersAsync(request);
        var baseResponsePagination = _mapper.Map<BaseResponsePagination<GetAllProviderResponse>>(result);
        baseResponsePagination.StatusCode = StatusCodes.Status200OK;
        baseResponsePagination.Message = "Get Providers successfully";
        return Ok(baseResponsePagination);
    }
    
}