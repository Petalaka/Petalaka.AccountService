
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels.ProviderRequest;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.ProviderResponse;
using Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.ProviderFilters;
using Petalaka.Account.Contract.Repository.QueryOptions.RequestOptions;
using Petalaka.Account.Contract.Repository.QueryOptions.SortOptions.ProviderSorts;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IProviderService
{
    Task CreateProviderAsync(CreateProviderRequest request);

    Task<PaginationResponse<GetAllProviderResponse>> GetProvidersAsync(
        RequestOptionsBase<GetAllProvidersFilterOptions, GetAllProviderSortOptions> request);
}