namespace PicPayBackendChallenge.Services;
using PicPayBackendChallenge.Models;

public interface ITransactionService
{
    public  Task<Transaction> CreateTransaction(Transaction transaction);
}