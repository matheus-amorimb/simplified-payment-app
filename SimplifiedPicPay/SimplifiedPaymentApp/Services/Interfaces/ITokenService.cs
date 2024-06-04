using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimplifiedPicPay.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();
}