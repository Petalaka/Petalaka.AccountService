using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
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
    
    [HttpPost]
    [Route("v1/provider")]
    public async Task<IActionResult> CreateProviderAsync([FromBody] CreateProviderRequest request)
    {
        await _providerService.CreateProviderAsync(request);
        return Ok();
    }
}