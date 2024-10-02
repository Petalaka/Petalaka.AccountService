using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.RoleRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.RoleResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IRoleService
{
    Task CreateRoleAsync(CreateRoleRequestModel request);
    Task<IEnumerable<GetRoleResponseModel>> GetRolesAsync();
}