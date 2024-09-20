using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAccountService
{
    Task RegisterAccount(RegisterRequestModel request);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<LoginResponseModel> Login(LoginRequestModel request);
}