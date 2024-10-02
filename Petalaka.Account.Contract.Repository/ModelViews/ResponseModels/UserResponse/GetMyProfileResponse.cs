namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;

public class GetMyProfileResponse
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
}