using Petalaka.Account.Contract.Repository.QueryOptions.ExtensionOptions;

namespace Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.ProviderFilters;

public class GetAllProvidersFilterOptions
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public TimeRange? CreateTimeRange { get; set; }
}