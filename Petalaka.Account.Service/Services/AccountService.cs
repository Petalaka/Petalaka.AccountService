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
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IBus _bus;
    
    public AccountService
    (
        UserManager<ApplicationUser> userManager, 
        IUnitOfWork unitOfWork,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService, 
        IPublishEndpoint publishEndpoint,
        RoleManager<ApplicationRole> roleManager,
        IBus bus
        )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _publishEndpoint = publishEndpoint;
        _roleManager = roleManager;
        _bus = bus;
    }

    public async Task RegisterAccount(RegisterRequestModel request)
    {
        ApplicationUser user = await _unitOfWork.ApplicationUserRepository.FindUndeletedAsync(p => p.Email == request.Email || p.UserName == request.Username);
        if(user != null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User already exists");
        }

        var roleName = StringConverterHelper.CapitalizeString("user");
        //Get role by identity role (_roleManager)
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Role not found");
        }
        
        //Generate salt and hash password with salt
        string salt = PasswordHasher.GenerateSalt();
        string hashedPassword = PasswordHasher.HashPassword(request.Password, salt);
        string emailOtp = OtpGenerator.GenerateOtp();
        string emailOtpExpiry = CoreHelper.GenerateTimeStampOtp;
        ApplicationUser newUser = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FullName = request.FullName,
            CreatedBy = "System",
            LastUpdatedBy = "System",
            Salt = salt,
            PasswordHash = hashedPassword,
            EmailOtp = emailOtp,
            EmailOtpExpiration = emailOtpExpiry
        };
        //create user with identity
        await _userManager.CreateAsync(newUser, hashedPassword);
        await _userManager.AddToRoleAsync(newUser, roleName);
        /*
        await _unitOfWork.ApplicationUserRepository.InsertAsync(newUser);
        */
        await _unitOfWork.SaveChangesAsync();
        
        //send email verification
        var message = new EmailVerificationEvent
        {
            Email = request.Email,
            EmailOtp = emailOtp
        };
        await _publishEndpoint.Publish<IEmailVerificationEvent>(message);
    }
    
    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        return await _unitOfWork.ApplicationUserRepository.AsQueryable().ToListAsync();
    }
    
    /// <summary>
    /// Login and return AccessToken and RefreshToken by JWT, need to verify user password (get from request) with salt (get from database)
    /// by hash them and compare with identity password function
    /// </summary>
    /// Also need to verify email is confirmed
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="CoreException"></exception>
    public async Task<LoginResponseModel> Login(LoginRequestModel request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
        if(user == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "User not found");
        }

        //check if email is confirmed
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email is not confirmed");
        }
        /*if(!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.Salt))
        {
            _signInManager.CheckPasswordSignInAsync()
            throw new CoreException(StatusCodes.Status400BadRequest, "Password is incorrect");
        }*/
        
        //get salt from database
        string salt = await _unitOfWork.ApplicationUserRepository.GetUserSalt(p => p.Email == request.Email);
        //hash password with salt
        string hashedPassword = PasswordHasher.HashPassword(request.Password, salt);
        //check if hashedPassword with salt is equal to passwordHash in database by identity (hashedPassword in database is generated by identity)
        var resultSucceeded = await _signInManager.CheckPasswordSignInAsync(user, hashedPassword, true);
        if(!resultSucceeded.Succeeded)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Password is incorrect");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, hashedPassword, true, false);
        if(!signInResult.Succeeded)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Login failed");
        }

        var token = await _tokenService.GenerateTokens(user);
        return new LoginResponseModel
        {
            AccessToken = token.accessToken,
            RefreshToken = token.refreshToken,
            Role = await _userManager.GetRolesAsync(user)
        };
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
        if(String.CompareOrdinal(user.EmailOtpExpiration, CoreHelper.GenerateTimeStamp) < 0 )
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Email OTP is expired");
        }
        user.EmailConfirmed = true;
        user.EmailOtp = null;
        await _userManager.UpdateAsync(user);
    }    
}