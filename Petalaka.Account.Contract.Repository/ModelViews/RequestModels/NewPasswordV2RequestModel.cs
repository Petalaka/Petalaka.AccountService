using System.ComponentModel.DataAnnotations;

namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class NewPasswordForgotV2RequestModel
{
    [EmailAddress]
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ResetPasswordToken { get; set; }
    public string ExpiredTimeStamp { get; set; }
}