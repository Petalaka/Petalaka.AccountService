using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly PetalakaDbContext _dbContext;
    private readonly IApplicationUserRepository _applicationUserRepository;
    public UnitOfWork(PetalakaDbContext dbContext,
        IApplicationUserRepository applicationUserRepository) : base(dbContext)
    {
        _dbContext = dbContext;
        _applicationUserRepository = applicationUserRepository;
    }
    
    public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository;
   
    
}