using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels.AuthenticationResponse;

namespace Petalaka.Account.Contract.Service.Interface;

public interface ITokenService
{
    Task<string> GenerateAccessToken(ApplicationUser user, string deviceId);
    Task<TokenResponseModel> GenerateTokens(ApplicationUser user, string deviceId);
    Task<string> GenerateRefreshToken(ApplicationUser user, string deviceId);
    bool ValidateTokenExpired(string token);
    bool ValidTokenHash(string token, string inputString);
}