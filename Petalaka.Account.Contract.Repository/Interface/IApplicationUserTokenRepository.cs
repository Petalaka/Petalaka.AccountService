using System.Linq.Expressions;
using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IApplicationUserTokenRepository : IGenericRepository<ApplicationUserToken>
{
    Task<ApplicationUserToken?> GetUserAsync(Expression<Func<ApplicationUserToken, bool>> predicate);
}