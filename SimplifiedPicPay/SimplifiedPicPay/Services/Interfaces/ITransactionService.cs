using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Services;
using SimplifiedPicPay.Models;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetTransactionsByClient(Guid clientId);
    public  Task<Transaction> CreateTransaction(Transaction transaction);
}