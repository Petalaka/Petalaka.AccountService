using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.RoleResponse;

namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;

public class GetAllUserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? PhoneNumberConfirmed { get; set; }
    public string? GoogleId { get; set; }
    public string? Avatar { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}