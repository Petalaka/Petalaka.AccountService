namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;

public class UpdateMyProfileRequest
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
}