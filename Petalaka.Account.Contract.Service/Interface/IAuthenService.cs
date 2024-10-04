using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAuthenService
{
    Task RegisterAccount(RegisterRequestModel request);
    Task<LoginResponseModel> Login(LoginRequestModel request);
    Task<LoginResponseModel> LoginWithGoogle(string googleId, string email);
}