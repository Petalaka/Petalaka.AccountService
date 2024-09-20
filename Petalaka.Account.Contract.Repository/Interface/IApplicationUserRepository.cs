using System.Linq.Expressions;
using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Contract.Repository.Interface;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    Task<string> GetUserSalt(Expression<Func<ApplicationUser, bool>> predicate);
}