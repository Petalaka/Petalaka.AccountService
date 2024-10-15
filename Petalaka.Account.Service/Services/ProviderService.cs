using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.ProviderResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.ProviderFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.ProviderSorts;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;
using Petalaka.Account.Service.Events.ProviderEvent;
using Petalaka.Service.Shared.RabbitMQ.Events.Interfaces.ProviderEvent;

namespace Petalaka.Account.Service.Services;

public class ProviderService : IProviderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ProviderService(IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IPublishEndpoint publishEndpoint,
        RoleManager<ApplicationRole> roleManager
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _publishEndpoint = publishEndpoint;
        _roleManager = roleManager;
    }

    public async Task CreateProviderAsync(CreateProviderRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email already exists");
        }

        var roleName = StringConverterHelper.CapitalizeString("provider");
        //Get role by identity role (_roleManager)
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Role not found");
        }
        
        var user = _mapper.Map<ApplicationUser>(request);
        user.UserName = request.Email;
        string password = PasswordHasher.GenerateSecurePassword();
        string salt = PasswordHasher.GenerateSalt();
        string hashedPassword = PasswordHasher.HashPassword(password, salt);

        user.Salt = salt;
        user.EmailConfirmed = true;
        IdentityResult identityResult = await _userManager.CreateAsync(user, hashedPassword);
        if (!identityResult.Succeeded)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, identityResult.Errors.First().Description);
        }

        Provider provider = _mapper.Map<Provider>(request);
        provider.UserId = user.Id;

        await _unitOfWork.GetRepository<Provider>().InsertAsync(provider);
        await _unitOfWork.SaveChangesAsync();

        user.ProviderId = provider.Id;
        await _userManager.UpdateAsync(user);
        await _userManager.AddToRoleAsync(user, roleName);
        IPasswordCreatedProviderEvent message = new PasswordCreatedProviderEvent
        {
            Email = user.Email,
            Password = password
        };
        await _publishEndpoint.Publish(message);
    }

    public async Task<PaginationResponse<GetAllProviderResponse>> GetProvidersAsync(
        RequestOptionsBase<GetAllProvidersFilterOptions, GetAllProviderSortOptions> request)
    {
        var providerQuery = _unitOfWork.GetRepository<ApplicationUser>().AsQueryable();
        if (request.IsDelete == true)
        {
            providerQuery = providerQuery.Where(p => p.DeletedTime != null);
        }

        var roleName = "PROVIDER";
        var providerRolesQuery = _unitOfWork.GetRepository<ApplicationUserRoles>().AsQueryable();
        var rolesQuery = _unitOfWork.GetRepository<ApplicationRole>().AsQueryable();
        var providerTable = _unitOfWork.GetRepository<Provider>().AsQueryable();
        
        providerQuery = from user in providerQuery
            join userRole in providerRolesQuery on user.Id equals userRole.UserId
            join role in rolesQuery on userRole.RoleId equals role.Id
            join provider in providerTable on user.ProviderId equals provider.Id
            where role.Name == roleName
            select user;

        if (request.FilterOptions != null)
        {
            if (!String.IsNullOrWhiteSpace(request.FilterOptions.Name))
            {
                providerQuery = providerQuery.Where(p => p.FullName.Contains(request.FilterOptions.Name));
            }

            if (!String.IsNullOrWhiteSpace(request.FilterOptions.Email))
            {
                providerQuery = providerQuery.Where(p => p.Email.Contains(request.FilterOptions.Email));
            }

            if (!String.IsNullOrWhiteSpace(request.FilterOptions.ContactEmail))
            {
                providerQuery = providerQuery.Where(p =>
                    p.Provider.ContactEmail.Contains(request.FilterOptions.ContactEmail));
            }

            if (!String.IsNullOrWhiteSpace(request.FilterOptions.ContactPhone))
            {
                providerQuery = providerQuery.Where(p =>
                    p.Provider.ContactPhone.Contains(request.FilterOptions.ContactPhone));
            }

            if (request.FilterOptions.CreateTimeRange != null)
            {
                providerQuery = providerQuery.Where(p => p.CreatedTime >= request.FilterOptions.CreateTimeRange.From &&
                                                         p.CreatedTime <= request.FilterOptions.CreateTimeRange.To);
            }
        }

        switch (request.SortOptions)
        {
            case GetAllProviderSortOptions.CreatedTimeAscending:
                providerQuery = providerQuery.OrderBy(p => p.CreatedTime);
                break;
            case GetAllProviderSortOptions.CreatedTimeDescending:
                providerQuery = providerQuery.OrderByDescending(p => p.CreatedTime);
                break;
            default:
                providerQuery = providerQuery.OrderByDescending(p => p.CreatedTime);
                break;
        }

        var queryPaging = await _unitOfWork.GetRepository<ApplicationUser>()
            .GetPagination(providerQuery, request.PageNumber, request.PageSize);

        var providers = _mapper.Map<List<GetAllProviderResponse>>(queryPaging.Data);
        return new PaginationResponse<GetAllProviderResponse>(providers, queryPaging.PageNumber, queryPaging.PageSize,
            queryPaging.TotalRecords, queryPaging.CurrentPageRecords);
    }
}