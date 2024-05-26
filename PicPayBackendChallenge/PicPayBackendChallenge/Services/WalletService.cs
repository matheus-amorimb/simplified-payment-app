using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Repositories.Interfaces;

namespace PicPayBackendChallenge.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<Wallet> CreateWallet(Wallet wallet)
    {
        var response = await _walletRepository.Create(wallet);

        return response;
    }
}