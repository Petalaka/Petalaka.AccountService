using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Base.Interface;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Contract.Repository.Entities;

public class ApplicationUserClaim : IdentityUserClaim<Guid>, IBaseEntity
{
    
    public ApplicationUserClaim()
    {
        CreatedTime = CoreHelper.SystemTimeNow;
        LastUpdatedTime = CreatedTime;
    }

    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}