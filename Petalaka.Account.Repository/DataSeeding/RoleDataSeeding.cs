using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Repository.DataSeeding;

public static class RoleDataSeeding
{
    public static IList<ApplicationRole> DefaultRoles => new List<ApplicationRole>
    {
        new ApplicationRole
        {
            Id = new Guid("B74C0A77-A451-4F16-DE61-08DCDFCDB851"),
            Name = "ADMIN",
            NormalizedName = "ADMIN",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow,
            ConcurrencyStamp = "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US"
        },
        new ApplicationRole
        {
            Id = new Guid("89FCA251-F021-425B-DE62-08DCDFCDB851"),
            Name = "USER",
            NormalizedName = "USER",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow,
            ConcurrencyStamp = "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US"
        },
        new ApplicationRole
        {
            Id = new Guid("D14E804F-1132-4923-DE63-08DCDFCDB851"),
            Name = "PROVIDER",
            NormalizedName = "PROVIDER",
            CreatedBy = "System",
            LastUpdatedBy = "System",
            CreatedTime = CoreHelper.SystemTimeNow,
            LastUpdatedTime = CoreHelper.SystemTimeNow,
            ConcurrencyStamp = "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US"

        }
    };
}