using SimplifiedPicPay.Dtos;

namespace SimplifiedPicPay.Services;

public interface ITransactionAuthService
{
    Task<bool> IsAuthorized();
}