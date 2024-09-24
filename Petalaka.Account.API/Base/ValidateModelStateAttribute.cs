using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Petalaka.Account.Core.ExceptionCustom;

namespace Petalaka.Account.API.Base;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Check ModelState
        if (!context.ModelState.IsValid)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, context.ModelState.ToString());
        }

        // Call the base class method
        base.OnActionExecuting(context);
    }
}