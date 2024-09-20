using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<ApplicationRole, GetRoleResponseModel>().ReverseMap();
    }
}