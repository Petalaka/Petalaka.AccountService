using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.UserResponse;

namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.ProviderResponse;

public class GetAllProviderResponse
{
    public string? Id { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
    public GetAllUserResponse? Account { get; set; }

}