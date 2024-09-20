using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IRoleService
{
    Task CreateRoleAsync(CreateRoleRequestModel request);
    Task<IEnumerable<GetRoleResponseModel>> GetRolesAsync();
}