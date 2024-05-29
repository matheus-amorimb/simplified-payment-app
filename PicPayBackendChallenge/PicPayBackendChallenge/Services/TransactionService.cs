using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Services;

public class TransactionService : ITransactionService
{
    private WalletService _walletService;

    public TransactionService(WalletService walletService)
    {
        _walletService = walletService;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        Wallet payerWallet = await _walletService.GetWalletById(transaction.PayerId);
        
        //Check if payer is a user
        this.IsPayerValid(payerWallet, transaction.PayerId);

        //check if user balance is equal ou greater than current balance
        this.IsPayerBalanceValid(payerWallet, transaction.Value);
        
        throw new NotImplementedException();
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
        if (wallet.WalletTypeId == 2)
        {
            throw new BadHttpRequestException("Payer cannot be of the type Merchant");
        }
        
    }
}