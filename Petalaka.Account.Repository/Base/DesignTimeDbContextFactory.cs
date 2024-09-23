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
        var _configuration = ReadConfiguration.ReadDbDesignTimeAppSettings();
        //var configuration = CoreHelper.GetDbDesignTimeAppSettings;
        var builder = new DbContextOptionsBuilder<PetalakaDbContext>();
        builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        return new PetalakaDbContext(builder.Options);
    }
}