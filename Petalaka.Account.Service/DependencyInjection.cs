using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Service.Services;

namespace Petalaka.Account.Service;

public static class DependencyInjection
{
    public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}