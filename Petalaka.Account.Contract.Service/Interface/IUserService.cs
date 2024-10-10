using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.UserFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.UserSorts;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IUserService
{
    Task<GetMyProfileResponse> GetMyProfile(string userId);
    Task UpdateMyProfile(UpdateMyProfileRequest request);

    Task<PaginationResponse<GetAllUserResponse>> GetUsers(
        RequestOptionsBase<GetAllUserFilterOptions, GetAllUserSortoptions> request);
}