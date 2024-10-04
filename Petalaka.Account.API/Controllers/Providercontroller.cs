using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class Providercontroller : BaseController
{
    private readonly IProviderService _providerService;
    public Providercontroller(IProviderService providerService)
    {
        _providerService = providerService;
    }
    
    /// <summary>
    /// Create Account for Provider
    /// </summary>
    /// <remarks>
    /// Require authentication as admin role to perform this function
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
}