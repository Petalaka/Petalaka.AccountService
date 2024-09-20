using Microsoft.AspNetCore.Http;

namespace Petalaka.Account.Core.ExceptionCustom;

public class CoreException : Exception
{
    public CoreException(int statusCode, string errorMessage)
        : base(errorMessage)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }    
}