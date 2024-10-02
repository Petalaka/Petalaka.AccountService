namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;

public class TokenResponseModel
{
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}