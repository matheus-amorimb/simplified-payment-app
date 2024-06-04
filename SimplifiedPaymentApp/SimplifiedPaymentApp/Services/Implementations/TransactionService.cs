using SimplifiedPicPay.Repositories.Implementations;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Enums;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Repositories.Interfaces;
using WalletType = SimplifiedPicPay.Enums.WalletType;

namespace SimplifiedPicPay.Services;

public class TransactionService : ITransactionService
{
    private readonly IWalletService _walletService;

    private readonly ITransactionRepository _transactionRepository;

    private readonly RabbitMqService _rabbitMqService;

    private readonly HttpClient _httpClient;

    private readonly ITransactionAuthService _transactionAuthService;

    public TransactionService(IWalletService walletService, ITransactionRepository transactionRepository, RabbitMqService rabbitMqService, HttpClient httpClient, ITransactionAuthService transactionAuthService)
    {
        _walletService = walletService;
        _transactionRepository = transactionRepository;
        _rabbitMqService = rabbitMqService;
        _httpClient = httpClient;
        _transactionAuthService = transactionAuthService;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByWallet(Guid clientWalletId)
    {
        IEnumerable<Transaction?> transactions = await _transactionRepository.GetByClientWallet(clientWalletId);

        return transactions;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        Wallet payerWallet = await _walletService.GetWalletById(transaction.PayerWalletId);
        Wallet payeeWallet = await _walletService.GetWalletById(transaction.PayeeWalletId);
        
        ValidateWallet(payerWallet, nameof(TransactionParticipant.Payer));
        
        ValidateWallet(payeeWallet, nameof(TransactionParticipant.Payee));

        ValidateSufficientBalance(payerWallet, transaction.Value);

        await _transactionAuthService.IsAuthorized();

        await UpdateWalletBalance(payerWallet, -transaction.Value);
        
        await UpdateWalletBalance(payeeWallet, transaction.Value);

        await _transactionRepository.Create(transaction);

        var transactionNotificationDto = new TransactionNotificationDto(transaction, payerWallet, payeeWallet);
        
        _rabbitMqService.Publish(transactionNotificationDto, "transaction-confirmation", "transaction-confirmation");

        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactions()
    {
        IEnumerable<Transaction?> transactions = await _transactionRepository.GetAll();
        return transactions;
    }

    private async Task<Wallet> UpdateWalletBalance(Wallet wallet, float amount)
    {
        wallet.Balance += amount;
        await _walletService.Uptade(wallet);
        return wallet;
    }   
    
    private void ValidateSufficientBalance(Wallet wallet, float amount)
    {
        if (wallet.Balance < amount)
        {
            throw new BadHttpRequestException("Payer has insufficient balance for this transaction");
        }
    }

    public void ValidateWallet(Wallet wallet, string walletType)
    {
        if (wallet == null)
        {
            throw new BadHttpRequestException($"{walletType} Id doesn't exist");
        }

        if (walletType == nameof(TransactionParticipant.Payer) && !wallet.IsUser)
        {
            throw new BadHttpRequestException("Payer cannot be of the type Merchant");
        }
    }
    
}