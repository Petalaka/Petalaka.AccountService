using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<ApplicationUser, GetMyProfileResponse>().ReverseMap();
        CreateMap<UpdateMyProfileRequest, ApplicationUser>();
    }
}