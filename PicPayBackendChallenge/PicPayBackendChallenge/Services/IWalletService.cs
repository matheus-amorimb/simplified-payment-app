using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Services;

public interface IWalletService
{
    Task<Wallet> CreateWallet(Wallet wallet);
}