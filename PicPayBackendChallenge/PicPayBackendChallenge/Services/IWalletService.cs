using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Services;

public interface IWalletService
{
    Task<IEnumerable<Wallet>> GetWallets();
    Task<Wallet> CreateWallet(Wallet wallet);
}