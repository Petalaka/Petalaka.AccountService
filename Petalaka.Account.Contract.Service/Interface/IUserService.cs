using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IUserService
{
    Task<GetMyProfileResponse> GetMyProfile(string userId);
    Task UpdateMyProfile(UpdateMyProfileRequest request);
}