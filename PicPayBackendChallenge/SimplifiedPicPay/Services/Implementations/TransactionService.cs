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

    public TransactionService(IWalletService walletService, ITransactionRepository transactionRepository, RabbitMqService rabbitMqService)
    {
        _walletService = walletService;
        _transactionRepository = transactionRepository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByClient(Guid clientId)
    {
        IEnumerable<Transaction?> transactions = await _transactionRepository.GetByClient(clientId);

        return transactions;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        Wallet payerWallet = await _walletService.GetWalletById(transaction.PayerId);
        Wallet payeeWallet = await _walletService.GetWalletById(transaction.PayeeId);
        
        //Check if payer exixts and it is a user
        ValidateWallet(payerWallet, nameof(TransactionParticipant.Payer));
        ValidateWallet(payeeWallet, nameof(TransactionParticipant.Payee));

        //check if user balance is equal ou greater than current balance
        ValidateSufficientBalance(payerWallet, transaction.Value);
        
        //Update Payer balance
        await UpdateWalletBalance(payerWallet, -transaction.Value);
        //Update Payee balance
        await UpdateWalletBalance(payeeWallet, transaction.Value);

        await _transactionRepository.Create(transaction);

        var transactionNotificationDto = new TransactionNotificationDto(transaction, payerWallet, payeeWallet);
        
        _rabbitMqService.Publish(transactionNotificationDto, "transaction-confirmation", "transaction-confirmation");

        return transaction;
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