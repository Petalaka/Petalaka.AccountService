using System.ComponentModel.DataAnnotations;

namespace Petalaka.Account.Contract.Repository.Base.Interface;

public interface IBaseEntity
{
    string? CreatedBy { get; set; }
    string? LastUpdatedBy { get; set; }
    string? DeletedBy { get; set; }
    DateTimeOffset CreatedTime { get; set; }
    DateTimeOffset LastUpdatedTime { get; set; }
    DateTimeOffset? DeletedTime { get; set; }
}