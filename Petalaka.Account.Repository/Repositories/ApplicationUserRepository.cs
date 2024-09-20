using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
{
    private readonly PetalakaDbContext _dbContext;
    public ApplicationUserRepository(PetalakaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetUserSalt(Expression<Func<ApplicationUser, bool>> predicate)
    {
        return await AsQueryableUndeletedPredicate(predicate)
            .Select(p => p.Salt)
            .FirstOrDefaultAsync();
    }
}