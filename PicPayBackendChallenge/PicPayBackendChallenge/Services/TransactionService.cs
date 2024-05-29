using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Repositories.Implementations;
using PicPayBackendChallenge.Repositories.Interfaces;

namespace PicPayBackendChallenge.Services;

public class TransactionService : ITransactionService
{
    private IWalletService _walletService;

    private ITransactionRepository _transactionRepository;

    public TransactionService(IWalletService walletService, ITransactionRepository transactionRepository)
    {
        _walletService = walletService;
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        Wallet payerWallet = await _walletService.GetWalletById(transaction.PayerId);
        
        Wallet payeeWallet = await _walletService.GetWalletById(transaction.PayeeId);
        
        //Check if payer exixts and it is a user
        this.IsPayerValid(payerWallet, transaction.PayerId);

        //check if user balance is equal ou greater than current balance
        this.IsPayerBalanceValid(payerWallet, transaction.Value);
        
        //Update Payer balance
        await this.UpdatePayerBalance(payerWallet, transaction.Value);
        
        //Update Payee balance
        await this.UpdatePayeeBalance(payeeWallet, transaction.Value);

        await _transactionRepository.Create(transaction);

        return transaction;
    }

    private async Task<Wallet> UpdatePayeeBalance(Wallet payeedIdWallet, float transactionValue)
    {
        payeedIdWallet.Balance += transactionValue;
        await _walletService.Uptade(payeedIdWallet);
        return payeedIdWallet;
    }   

    private async Task<Wallet> UpdatePayerBalance(Wallet payerIdWallet, float transactionValue)
    {
        payerIdWallet.Balance -= transactionValue;
        await _walletService.Uptade(payerIdWallet);
        return payerIdWallet;
    }

    private void IsPayerBalanceValid(Wallet payerWallet, float transactionValue)
    {
        if (payerWallet.Balance < transactionValue)
        {
            throw new BadHttpRequestException("Payer has insufficient balance for this transaction");
        }
    }

    public void IsPayerValid(Wallet wallet, Guid payerId)
    {
        if (wallet == null)
        {
            throw new BadHttpRequestException("Payer Id doesn't exist");
        }
        
        if (wallet.WalletTypeId == 2)
        {
            throw new BadHttpRequestException("Payer cannot be of the type Merchant");
        }
        
    }
}