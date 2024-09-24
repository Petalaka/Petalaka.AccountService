using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAccountService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task ConfirmEmail(ConfirmEmailRequestModel request);
    Task SendEmailOtp(ResendEmailConfirmationRequestModel request);
}