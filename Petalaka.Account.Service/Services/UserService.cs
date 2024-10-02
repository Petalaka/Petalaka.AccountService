using AutoMapper;
using Microsoft.AspNetCore.Http;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<GetMyProfileResponse> GetMyProfile(string userId)
    {
        var user = await _unitOfWork.GetRepository<ApplicationUser>()
            .FindUndeletedAsync(p => p.Id.ToString() == StringConverterHelper.CapitalizeString(userId));
        if (user == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User not found");
        }
        return _mapper.Map<GetMyProfileResponse>(user); 
    }

    public async Task UpdateMyProfile(UpdateMyProfileRequest request)
    {
        var user = await _unitOfWork.GetRepository<ApplicationUser>()
            .FindUndeletedAsync(p => p.Id.ToString() == StringConverterHelper.CapitalizeString(request.Id));
        if (user == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User not found");
        }
        _mapper.Map(request, user);
        _unitOfWork.GetRepository<ApplicationUser>().Update(user);
        await _unitOfWork.SaveChangesAsync();
    }
}