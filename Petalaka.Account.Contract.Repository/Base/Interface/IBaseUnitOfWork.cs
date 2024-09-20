namespace Petalaka.Account.Contract.Repository.Base.Interface;

public interface IBaseUnitOfWork
{
    Task SaveChangesAsync();
    void SaveChanges();
    void Dispose();
    
}