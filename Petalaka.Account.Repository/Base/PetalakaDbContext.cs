using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Repository.DataSeeding;

namespace Petalaka.Account.Repository.Base;

public class PetalakaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRoles, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>
{
    public PetalakaDbContext(DbContextOptions<PetalakaDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<Provider> Providers => Set<Provider>();
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
        modelBuilder.Entity<ApplicationUser>().HasData(AccountDataSeeding.DefaultUsers);
        modelBuilder.Entity<ApplicationRole>().HasData(RoleDataSeeding.DefaultRoles);
        modelBuilder.Entity<ApplicationUserRoles>().HasData(UserRoleDataSeeding.DefaultUserRoles);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}