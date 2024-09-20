using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Service.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RoleService(RoleManager<ApplicationRole> roleManager, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateRoleAsync(CreateRoleRequestModel request)
    {
        var role = await _roleManager.FindByNameAsync(StringConverterHelper.NormalizeString(request.RoleName));
        if(role != null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Role already exists");
        }
        var newRole = new ApplicationRole
        {
            Name = request.RoleName
        };
        await _roleManager.CreateAsync(newRole);
    }

    public async Task<IEnumerable<GetRoleResponseModel>> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return _mapper.Map<IEnumerable<GetRoleResponseModel>>(roles);
    }
    
}