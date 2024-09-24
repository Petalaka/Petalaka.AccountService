using AutoMapper;
using MassTransit;
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
using Petalaka.Account.Service.Events.AccountEvent;
using Petalaka.Service.Shared.RabbitMQ.Events.Interfaces;

namespace Petalaka.Account.Service.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper; 
    
    public AccountService
    (
        UserManager<ApplicationUser> userManager, 
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _unitOfWork.ApplicationUserRepository.AsQueryable().ToListAsync();
    }
    
    public async Task ConfirmEmail(ConfirmEmailRequestModel request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
        if(user == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User not found");
        }
        if(user.EmailConfirmed)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email is already confirmed");
        }
        if(user.EmailOtp != request.EmailOtp)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email OTP is incorrect");
        }
        if(String.CompareOrdinal(user.EmailOtpExpiration, TimeStampHelper.GenerateTimeStamp()) < 0 )
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email OTP is expired");
        }
        user.EmailConfirmed = true;
        user.EmailOtp = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task SendEmailOtp(ResendEmailConfirmationRequestModel request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
        if(user == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User not found");
        }
        user.EmailOtp = OtpGenerator.GenerateOtp();
        user.EmailOtpExpiration = TimeStampHelper.GenerateTimeStampOtp();
        await _userManager.UpdateAsync(user);
        IEmailOtpEvent message = new EmailOtpEvent
        {
            Email = user.Email,
            EmailOtp = user.EmailOtp
        };  
        await _publishEndpoint.Publish(message);
    }
}