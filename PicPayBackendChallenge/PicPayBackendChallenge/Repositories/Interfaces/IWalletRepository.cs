using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Repositories.Interfaces;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetAll();

    Task<Wallet> GetById(Guid id);

    Task<Wallet> Create(Wallet wallet);
    Task<Wallet> Update(Guid id, Wallet wallet);
    
}