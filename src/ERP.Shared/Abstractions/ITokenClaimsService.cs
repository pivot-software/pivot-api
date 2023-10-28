using System.Security.Claims;
using ERP.Shared.Records;

namespace ERP.Shared.Abstractions;

public interface ITokenClaimsService
{
    AccessToken GenerateAccessToken(Claim[] claims);
    string GenerateRefreshToken();
}
