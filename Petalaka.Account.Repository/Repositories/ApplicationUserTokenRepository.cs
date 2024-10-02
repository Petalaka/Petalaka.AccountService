using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class ApplicationUserTokenRepository : GenericRepository<ApplicationUserToken>, IApplicationUserTokenRepository
{
    private readonly PetalakaDbContext _dbContext;
    public ApplicationUserTokenRepository(PetalakaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ApplicationUserToken?> GetUserAsync(Expression<Func<ApplicationUserToken, bool>> predicate)
    {
        return await AsQueryableUndeletedPredicate(predicate)
            .FirstOrDefaultAsync();
    }
}