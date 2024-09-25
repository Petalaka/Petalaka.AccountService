namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

public class ConfirmForgotPasswordResponseModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}