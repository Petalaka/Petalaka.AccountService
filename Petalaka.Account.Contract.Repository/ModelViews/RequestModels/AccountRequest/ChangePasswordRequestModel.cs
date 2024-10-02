namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AccountRequest;

public class ChangePasswordRequestModel
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}