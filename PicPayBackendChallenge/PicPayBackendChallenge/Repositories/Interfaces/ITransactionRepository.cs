using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction?>> GetAll();
    Task<IEnumerable<Transaction?>> GetByClient(Guid userId);
    Task<Transaction?> GetById(Guid id);   
    Task<Transaction> Create(Transaction transaction);
    
}