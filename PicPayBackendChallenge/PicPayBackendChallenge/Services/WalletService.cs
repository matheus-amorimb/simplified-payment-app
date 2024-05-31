using PicPayBackendChallenge.Dtos;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Repositories.Interfaces;

namespace PicPayBackendChallenge.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    private readonly RabbitMqService _rabbitMqService;

    public WalletService(IWalletRepository walletRepository, RabbitMqService rabbitMqService)
    {
        _walletRepository = walletRepository;
        _rabbitMqService = rabbitMqService;
    }
    
    public async Task<Wallet> GetWalletById(Guid id)
    {
        var response = await _walletRepository.GetById(id);
        return response;
    }

    public async Task<IEnumerable<Wallet>> GetWallets()
    {
        var response = await _walletRepository.GetAll();
        return response;
    }

    public async Task<Wallet> CreateWallet(Wallet wallet)
    {

        IEnumerable<Wallet> wallets = await this.GetWallets();

        if (IsCpfInUse(wallets, wallet.Cpf))
        {
            throw new BadHttpRequestException("Email is already in use");
        }
        
        if (IsEmailInUse(wallets, wallet.Email))
        {
            throw new BadHttpRequestException("Cpf is already in use");
        }
        
        var response = await _walletRepository.Create(wallet);

        var walletNotificationDto = new WalletNotificationDto(wallet);
        
        _rabbitMqService.Publish(response, "wallet-creation-confirmation", "wallet-creation-confirmation");
        
        return response;
    }

    public async Task<Wallet> Uptade(Wallet wallet)
    {
        Wallet walletToUptade = await this.GetWalletById(wallet.WalletId);    
    
        if (walletToUptade == null)
        {
            throw new BadHttpRequestException("");
        }

        await _walletRepository.Update(wallet);

        return wallet;
    }

    public bool IsEmailInUse(IEnumerable<Wallet> wallets, string email)
    {   
        return wallets.Any(wallet => wallet.Email == email);
    }
    
    public bool IsCpfInUse(IEnumerable<Wallet> wallets, string cpf)
    {
        return wallets.Any(wallet => wallet.Cpf == cpf);
    }
}