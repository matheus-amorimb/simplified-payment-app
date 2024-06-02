using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Services;

public interface IWalletService
{
    Task<IEnumerable<Wallet>> GetWallets();
    Task<Wallet> GetWalletById(Guid id);
    Task<Wallet> CreateWallet(Wallet wallet);
    Task<Wallet> Uptade(Wallet wallet);
}