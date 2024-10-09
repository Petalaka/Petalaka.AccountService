using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.AuthenticationRequest;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IAuthenService
{
    Task RegisterAccount(RegisterRequestModel request);
    Task<LoginResponseModel> Login(LoginRequestModel request, string deviceId);
    Task<LoginResponseModel> LoginWithGoogle(string googleId, string email, string deviceId);
    Task Logout(string userId, string deviceId);
    Task<bool> ValidateToken(ValidateTokenRequestModel request);
}