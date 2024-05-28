using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Wallet>> GetAll()
    {
        IEnumerable<Wallet> wallets = await _context.Wallet.FromSql($"SELECT * FROM Wallet").ToListAsync();
        return wallets;
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