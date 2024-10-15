using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;

public class CreateProviderRequest
{
    [EmailAddress]
    public required string Email { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    [DataType(DataType.Date, ErrorMessage = "Invalid date of birth format")]
    public DateOnly? DateOfBirth { get; set; }
    [RegularExpression(@"^[\p{L}[\s]+$", ErrorMessage = "Full name should only contain letters and spaces, no special characters or numbers")]
    [DefaultValue("string")]
    public string? FullName { get; set; }
    [Phone(ErrorMessage = "Invalid phone number")]
    [DefaultValue("0909123456")]
    public string? PhoneNumber { get; set; }
    [EmailAddress]
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Invalid phone number")]
    [DefaultValue("0909123456")]
    public string? ContactPhone { get; set; }
}