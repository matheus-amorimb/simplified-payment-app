using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Services;

public interface IAuthService
{
    Task<UserRegisterRequestDto> Register(UserRegisterRequestDto userRegisterRequestDto);
}