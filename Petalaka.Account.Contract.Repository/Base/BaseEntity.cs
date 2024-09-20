using System.ComponentModel.DataAnnotations;
using Petalaka.Account.Contract.Repository.Base.Interface;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Contract.Repository.Base;

public abstract class BaseEntity : IBaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    protected BaseEntity()
    {
        CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
    }
}