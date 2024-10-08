using Petalaka.Account.Contract.Repository.QueryOptions.ExtensionOptions;

namespace Petalaka.Account.Contract.Repository.QueryOptions.FilterOptions.UserFilters;

public class GetAllUserFilterOptions
{
   public string Name { get; set; }
   public string Email { get; set; }
   public TimeRange CreateTimeRange { get; set; }
}