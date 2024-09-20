using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Base.Interface;
using Petalaka.Account.Core.ExceptionCustom;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Contract.Repository.Entities;

public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
{
    public string FullName { get; set; }
    public string Salt { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
    public ApplicationUser()
    {
        CreatedTime = CoreHelper.SystemTimeNow;
        LastUpdatedTime = CreatedTime;
    }
}