using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AccountRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AccountResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAccountService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task ConfirmEmail(ConfirmEmailRequestModel request);
    Task SendEmailOtp(ResendEmailConfirmationRequestModel request);
    Task ChangePassword(string email, ChangePasswordRequestModel request);

    Task<ConfirmForgotPasswordResponseModel> ForgotPassword(ForgotPasswordRequestModel request, string deviceId);

    Task NewPasswordForgot(string email, NewPasswordRequestModel request);
    Task ForgotPasswordV2(ForgotPasswordV2RequestModel request);
    Task NewPasswordForgotV2(NewPasswordForgotV2RequestModel request);
}