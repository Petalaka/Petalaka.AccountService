using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Petalaka.Account.API.Base;

[ApiController]
[Route("api/account-service/[controller]")]
[ServiceFilter(typeof(ValidateModelStateAttribute))]
public abstract class BaseController : ControllerBase
{
    
}