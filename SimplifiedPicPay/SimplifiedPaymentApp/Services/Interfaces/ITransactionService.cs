using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Services;
using SimplifiedPicPay.Models;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetTransactionsByWallet(Guid clientWalletId);
    public  Task<Transaction> CreateTransaction(Transaction transaction);
    public Task<IEnumerable<Transaction>> GetAllTransactions();
}