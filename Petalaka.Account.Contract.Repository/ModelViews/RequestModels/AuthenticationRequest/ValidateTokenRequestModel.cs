namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AuthenticationRequest;

public class ValidateTokenRequestModel
{
    public string UserId { get; set; }
    public string DevideId { get; set; }
    public string TokenHashed { get; set; }
}