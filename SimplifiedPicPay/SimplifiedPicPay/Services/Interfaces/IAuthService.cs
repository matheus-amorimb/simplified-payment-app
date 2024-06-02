using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Services;

public interface IAuthService
{
    Task<UserLoginResponseDto> LogIn(UserLoginRequestDto userLoginRequestDto);
    Task<UserRegisterRequestDto> Register(UserRegisterRequestDto userRegisterRequestDto);
}