using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ERP.Shared.Abstractions;
using ERP.Shared.AppSettings;
using ERP.Shared.Records;

namespace ERP.Infrastructure.Services;

public class JwtClaimService : ITokenClaimsService
{
    private const short RefreshTokenBytesLength = 64;
    private readonly IDateTimeService _dateTimeService;
    private readonly JwtOptions _jwtConfig;

    public JwtClaimService(IOptions<JwtOptions> jwtOptions, IDateTimeService dateTimeService)
    {
        _jwtConfig = jwtOptions.Value;
        _dateTimeService = dateTimeService;
    }

    public AccessToken GenerateAccessToken(Claim[] claims)
    {
        Guard.Against.NullOrEmpty(claims);

        var createdAt = _dateTimeService.Now;

        var expiresAt = createdAt.AddSeconds(_jwtConfig.Seconds);

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            NotBefore = createdAt,
            Expires = expiresAt,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = CreateSigningCredentials()
        });


        var token = tokenHandler.WriteToken(securityToken);
        return new AccessToken(token, createdAt, expiresAt);
    }

    public string GenerateRefreshToken()
    {

        using var rnd = RandomNumberGenerator.Create();
        var randomBytes = new byte[RefreshTokenBytesLength];
        rnd.GetBytes(randomBytes);

        string refreshToken = Convert.ToBase64String(randomBytes);


        return refreshToken;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var secretKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var securityKey = new SymmetricSecurityKey(secretKey);
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}