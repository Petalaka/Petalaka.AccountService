
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IProviderService
{
    Task CreateProviderAsync(CreateProviderRequest request);
}