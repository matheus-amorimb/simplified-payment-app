using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Services;

public interface IWalletService
{
    Task<IEnumerable<Wallet>> GetWallets();
    Task<Wallet> GetWalletById(Guid id);
    Task<WalletResponseDto> CreateWallet(WalletRequestDto walletRequestDto);
    Task<Wallet> Uptade(Wallet wallet);
}