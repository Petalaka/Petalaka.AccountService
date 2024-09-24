using System.ComponentModel.DataAnnotations;

namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class RegisterRequestModel
{
    public required string Password { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    [DataType(DataType.DateTime, ErrorMessage = "Invalid date of birth format")]
    public DateTime DateOfBirth { get; set; }
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name should only contain letters and spaces, no special characters or numbers")]
    public string FullName { get; set; }
    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
}