using AutoMapper;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Repositories.Interfaces;

namespace SimplifiedPicPay.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    private readonly RabbitMqService _rabbitMqService;

    private readonly IMapper _mapper;

    public WalletService(IWalletRepository walletRepository, RabbitMqService rabbitMqService, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _rabbitMqService = rabbitMqService;
        _mapper = mapper;
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

    public async Task<WalletResponseDto> CreateWallet(WalletRequestDto walletRequestDto)
    {

        IEnumerable<Wallet> wallets = await this.GetWallets();

        Wallet wallet = _mapper.Map<Wallet>(walletRequestDto);
        
        ValidateEmail(wallets, wallet.Email);

        Console.WriteLine(wallet.FullName);
        
        var response = await _walletRepository.Create(wallet);
        
        WalletResponseDto responseDto = _mapper.Map<WalletResponseDto>(response);

        var walletNotificationDto = new WalletNotificationDto(wallet);
        
        _rabbitMqService.Publish(walletNotificationDto, "wallet-creation-confirmation", "wallet-creation-confirmation");
        
        return responseDto;
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

    public void ValidateEmail(IEnumerable<Wallet> wallets, string email)
    {
        if (wallets.Any(wallet => wallet.Email == email))
        {
            throw new BadHttpRequestException("Email is already in use");
        }
    }
    
}