using System.ComponentModel.DataAnnotations;

namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AccountRequest;

public class ConfirmEmailRequestModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }
    [Required]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "The value must be exactly 6 digits.")]
    public string EmailOtp { get; set; }
}