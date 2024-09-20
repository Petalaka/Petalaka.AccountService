using Petalaka.Account.Contract.Repository.Base.Interface;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IUnitOfWork : IDisposable, IBaseUnitOfWork
{
    IApplicationUserRepository ApplicationUserRepository { get; }
}