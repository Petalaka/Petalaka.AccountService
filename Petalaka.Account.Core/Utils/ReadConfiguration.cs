using Microsoft.Extensions.Configuration;

namespace Petalaka.Account.Core.Utils;

public static class ReadConfiguration
{
    public static IConfiguration ReadAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .Build();
        return configuration;
    }
    public static IConfiguration ReadBasePathAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Petalaka.Account.API")))
            .AddJsonFile("appsettings.Development.json")
            .Build();
        return configuration;
    }
}