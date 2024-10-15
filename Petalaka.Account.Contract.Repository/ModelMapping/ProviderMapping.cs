using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.ProviderResponse;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class ProviderMapping : Profile
{
    public ProviderMapping()
    {
        CreateMap<CreateProviderRequest, ApplicationUser>();
        CreateMap<CreateProviderRequest, Provider>();
        CreateMap<Provider, GetAllProviderResponse>();
        CreateMap<ApplicationUser, GetAllProviderResponse>()
            .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src)) // Map the entire ApplicationUser to Account
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Provider.ContactEmail))
            .ForMember(dest => dest.ContactPhone, opt => opt.MapFrom(src => src.Provider.ContactPhone))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.Provider.CreatedTime))
            .ForMember(dest => dest.DeletedTime, opt => opt.MapFrom(src => src.Provider.DeletedTime))
            .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => src.Provider.LastUpdatedTime))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Provider.Id)); // Map Provider.Id to GetAllProviderResponse.Id

    }
}