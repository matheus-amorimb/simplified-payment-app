using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction?>> GetAll();
    Task<IEnumerable<Transaction?>> GetByClientWallet(Guid walletId);
    Task<Transaction?> GetById(Guid id);   
    Task<Transaction> Create(Transaction transaction);
    
}