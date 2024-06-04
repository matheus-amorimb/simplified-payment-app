using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Repositories.Interfaces;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetAll();

    Task<Wallet> GetById(Guid id);
    Task<Wallet> GetByUserId(Guid userId);

    Task<Wallet> Create(Wallet wallet);
    
    Task<Wallet> Update(Wallet wallet);
    
}