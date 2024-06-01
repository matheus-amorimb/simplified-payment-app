using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Services;

public interface IAuthService
{
    Task Register(UserRegisterRequestDto userRegisterRequestDto);
}