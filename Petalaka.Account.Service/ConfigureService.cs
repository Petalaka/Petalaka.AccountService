using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Service;

public static class ConfigureService
{
    public static void AddConfigureServiceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyInjectionService(configuration);
    }
    
  
}