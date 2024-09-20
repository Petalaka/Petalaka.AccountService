namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

public class LoginResponseModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}