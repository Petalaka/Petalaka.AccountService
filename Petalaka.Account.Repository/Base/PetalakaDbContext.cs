using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Repository.Base;

public class PetalakaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRoles, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>
{
    public PetalakaDbContext(DbContextOptions<PetalakaDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            string tableName = entityType.GetTableName()??"";
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}