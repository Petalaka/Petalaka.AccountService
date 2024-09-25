using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Repository.DataSeeding;

public static class RoleDataSeeding
{
    public static IList<ApplicationRole> DefaultRoles => new List<ApplicationRole>
    {
        new ApplicationRole
        {
            Id = new Guid(),
            Name = "ADMIN",
            NormalizedName = "ADMIN",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow
            
        },
        new ApplicationRole
        {
            Id = new Guid(),
            Name = "USER",
            NormalizedName = "USER",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow,
        },
        new ApplicationRole
        {
            Id = new Guid(),
            Name = "PROVIDER",
            NormalizedName = "PROVIDER",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow,
        }
    };
}