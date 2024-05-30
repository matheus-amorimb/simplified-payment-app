namespace PicPayBackendChallenge.Services;
using PicPayBackendChallenge.Models;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetTransactionsByClient(Guid clientId);
    public  Task<Transaction> CreateTransaction(Transaction transaction);
}