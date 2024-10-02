namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;

public class LoginResponseModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public IList<string> Role { get; set; }
}