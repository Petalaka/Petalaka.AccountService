using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        CreateMap<ApplicationUser, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.Ignore());
    }
}