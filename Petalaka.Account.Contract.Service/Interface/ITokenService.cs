using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Service.Interface;

public interface ITokenService
{
    Task<string> GenerateAccessToken(ApplicationUser user);
    Task<TokenResponseModel> GenerateTokens(ApplicationUser user);
    
}