using Petalaka.Account.Contract.Repository.Base.Interface;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IUnitOfWork : IDisposable, IBaseUnitOfWork
{
    IApplicationUserRepository ApplicationUserRepository { get; }
    IApplicationRoleRepository ApplicationRoleRepository { get; }
    IApplicationUserTokenRepository ApplicationUserTokenRepository { get; }
    IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity;
}