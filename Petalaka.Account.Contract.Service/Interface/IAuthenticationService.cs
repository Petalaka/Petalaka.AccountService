using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAuthenticationService
{
    Task RegisterAccount(RegisterRequestModel request);
    Task<LoginResponseModel> Login(LoginRequestModel request);
}