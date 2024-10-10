using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.UserRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.UserFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.UserSorts;
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
    
    public async Task<PaginationResponse<GetAllUserResponse>> GetUsers(RequestOptionsBase<GetAllUserFilterOptions, GetAllUserSortoptions> request)
    {
        var userQuery = _unitOfWork.GetRepository<ApplicationUser>().AsQueryable();
        
        if (request.FilterOptions != null)
        {
            if (!String.IsNullOrWhiteSpace(request.FilterOptions.Name))
            {
                userQuery = userQuery.Where(p => p.FullName.Contains(request.FilterOptions.Name));
            }
            if (!String.IsNullOrWhiteSpace(request.FilterOptions.Email))
            {
                userQuery = userQuery.Where(p => p.Email.Contains(request.FilterOptions.Email));
            }
            if (request.FilterOptions.CreateTimeRange != null)
            {
                userQuery = userQuery.Where(p => p.CreatedTime >= request.FilterOptions.CreateTimeRange.From &&
                                                 p.CreatedTime <= request.FilterOptions.CreateTimeRange.To);
            }
        }
  
        switch (request.SortOptions)
        {
            case GetAllUserSortoptions.CreatedTimeAscending:
                userQuery = userQuery.OrderBy(p => p.CreatedTime);
                break;
            case GetAllUserSortoptions.CreatedTimeDescending:
                userQuery = userQuery.OrderByDescending(p => p.CreatedTime);
                break;
            default:
                userQuery = userQuery.OrderByDescending(p => p.CreatedTime);
                break;
        }

        var queryPaging = await _unitOfWork.GetRepository<ApplicationUser>()
            .GetPagination(userQuery, request.PageNumber, request.PageSize);
        
        var users = _mapper.Map<IList<GetAllUserResponse>>(queryPaging.Data);
        return new PaginationResponse<GetAllUserResponse>(users, queryPaging.PageNumber, queryPaging.PageSize, queryPaging.TotalRecords, queryPaging.CurrentPageRecords);
    }
}