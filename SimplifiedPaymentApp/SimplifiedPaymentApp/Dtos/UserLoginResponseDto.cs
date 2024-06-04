using System.IdentityModel.Tokens.Jwt;

namespace SimplifiedPicPay.Dtos;

public class UserLoginResponseDto
{
    public string Status { get; set; }

    public  string Token { get; set; }

    public string RefreshToken { get; set; }

    public DateTime Expiration { get; set; }
}