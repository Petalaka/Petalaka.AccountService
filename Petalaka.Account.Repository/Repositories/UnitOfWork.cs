using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly PetalakaDbContext _dbContext;
    private readonly IApplicationUserRepository _applicationUserRepository;
    public readonly IApplicationRoleRepository _applicationRoleRepository;
    public UnitOfWork(PetalakaDbContext dbContext,
        IApplicationUserRepository applicationUserRepository,
        IApplicationRoleRepository applicationRoleRepository
        ) : base(dbContext)
    {
        _dbContext = dbContext;
        _applicationUserRepository = applicationUserRepository;
        _applicationRoleRepository = applicationRoleRepository;
    }
    
    public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository;
    public IApplicationRoleRepository ApplicationRoleRepository => _applicationRoleRepository;  
    
}