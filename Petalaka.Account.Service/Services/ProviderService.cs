using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
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
    public ProviderService(IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IPublishEndpoint publishEndpoint
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task CreateProviderAsync(CreateProviderRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email already exists");
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
        
        IPasswordCreatedProviderEvent message = new PasswordCreatedProviderEvent
        {
            Email = user.Email,
            Password = password
        };
        await _publishEndpoint.Publish(message);
    }
}