namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class RegisterRequestModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    
}