using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SimplifiedPicPay.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private IConfigurationSection JwtSettings { get; set;}

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        JwtSettings = _configuration.GetSection("JWT");
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = CreateSecurityTokenDescriptor(claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[128];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomBytes);
        var refreshToken = Convert.ToBase64String(randomBytes);
        return refreshToken;
    }
    private SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims)
    {
        int tokenValidityInMinutes = JwtSettings.GetValue<int>("TokenValidityInMinutes");
        ClaimsIdentity subject = new ClaimsIdentity(claims);
        DateTime expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
        string? issuer = JwtSettings.GetValue<string>("ValidIssuer");
        string? audience = JwtSettings.GetValue<string>("ValidAudience");
        SigningCredentials signingCredentials = CreateSigningCredentials();

        return new SecurityTokenDescriptor()
        {
            Subject = subject,
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials
        };
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(JwtSettings.GetValue<string>("SecretKey") ?? string.Empty);

        return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
    }

}