using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Repository.Base;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PetalakaDbContext>
{
    public DesignTimeDbContextFactory()
    {
    }
    public PetalakaDbContext CreateDbContext(string[] args)
    {
        var configuration = ReadConfiguration.ReadDbDesignTimeAppSettings();
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace("connectionString"))
        {
            connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
        }
        //var configuration = CoreHelper.GetDbDesignTimeAppSettings;
        var builder = new DbContextOptionsBuilder<PetalakaDbContext>();
        builder.UseSqlServer(connectionString);
        return new PetalakaDbContext(builder.Options);
    }
}