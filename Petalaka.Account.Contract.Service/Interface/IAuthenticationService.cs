using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AuthenticationRequest;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAuthenticationService
{
    Task RegisterAccount(RegisterRequestModel request);
    Task<LoginResponseModel> Login(LoginRequestModel request, string deviceId);
    Task Logout(string userId, string deviceId);
}