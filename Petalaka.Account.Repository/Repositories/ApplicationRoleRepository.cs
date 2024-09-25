using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class ApplicationRoleRepository : GenericRepository<ApplicationRole>, IApplicationRoleRepository
{
    private readonly PetalakaDbContext _dbContext;
    public ApplicationRoleRepository(PetalakaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}