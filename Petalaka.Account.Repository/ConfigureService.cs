using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Petalaka.Account.Contract.Repository.CustomSettings;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelMapping;
using Petalaka.Account.Core.Utils;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository;

public static class ConfigureService
{
    public static async void AddConfigureServiceRepository(this IServiceCollection services, IConfiguration configuration)
    {
        /*services.ConfigSwagger();
        services.AddAuthenJwt(configuration);
        services.AddDatabase(configuration);
        services.AddServices();
        services.ConfigRoute();
        services.AddInitialiseDatabase();
        services.ConfigCors();
        //services.ConfigCorsSignalR();
        //services.RabbitMQConfig(configuration);
        services.JwtSettingsConfig(configuration);*/
        services.AddDatabase(configuration);
        services.AddDependencyInjectionRepository(configuration);
        services.AddAutoMapperConfig(configuration);
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PetalakaDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString ??
                                 throw new InvalidOperationException(
                                     "Connection string not found in appsettings.json"), 
                                     options => options.EnableRetryOnFailure()
                                     ).UseLazyLoadingProxies();
        });
    }

    public static void AddAutoMapperConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(RoleMapping));
    }

    public static async Task UseInitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
        await dbInitializer.InitializeAsync();
    }
}