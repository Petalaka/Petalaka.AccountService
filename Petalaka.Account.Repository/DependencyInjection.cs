using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Repositories;

namespace Petalaka.Account.Repository;

public static class DependencyInjection
{
    public static void AddDependencyInjectionRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
    }
}