using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class ProviderMapping : Profile
{
    public ProviderMapping()
    {
        CreateMap<CreateProviderRequest, ApplicationUser>();
        CreateMap<CreateProviderRequest, Provider>();
    }
}