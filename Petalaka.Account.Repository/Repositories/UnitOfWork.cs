using Petalaka.Account.Contract.Repository.Base.Interface;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly PetalakaDbContext _dbContext;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IApplicationRoleRepository _applicationRoleRepository;
    private readonly IApplicationUserTokenRepository _applicationUserTokenRepository;
    public UnitOfWork(PetalakaDbContext dbContext,
        IApplicationUserRepository applicationUserRepository,
        IApplicationRoleRepository applicationRoleRepository,
        IApplicationUserTokenRepository applicationUserTokenRepository
        ) : base(dbContext)
    {
        _dbContext = dbContext;
        _applicationUserRepository = applicationUserRepository;
        _applicationRoleRepository = applicationRoleRepository;
        _applicationUserTokenRepository = applicationUserTokenRepository;
    }
    
    public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository;
    public IApplicationRoleRepository ApplicationRoleRepository => _applicationRoleRepository;
    public IApplicationUserTokenRepository ApplicationUserTokenRepository => _applicationUserTokenRepository;

    public IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity
    {
        return new GenericRepository<T>(_dbContext);
    }
}