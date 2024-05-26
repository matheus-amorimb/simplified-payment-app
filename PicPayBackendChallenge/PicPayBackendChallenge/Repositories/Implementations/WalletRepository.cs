using PicPayBackendChallenge.Context;
using PicPayBackendChallenge.Models;
using PicPayBackendChallenge.Repositories.Interfaces;

namespace PicPayBackendChallenge.Repositories.Implementations;

public class WalletRepository : IWalletRepository
{
    private readonly AppDbContext _context;

    public WalletRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Wallet>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Wallet> Create(Wallet wallet)
    {
        await _context.Wallet.AddAsync(wallet);
        await _context.SaveChangesAsync();
        
        return wallet;
    }

    public Task<Wallet> Update(Guid id, Wallet wallet)
    {
        throw new NotImplementedException();
    }
}