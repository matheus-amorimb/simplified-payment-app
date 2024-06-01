using Microsoft.EntityFrameworkCore;
using SimplifiedPicPay.Context;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Repositories.Interfaces;

namespace SimplifiedPicPay.Repositories.Implementations;

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

    public async Task<Wallet> GetById(Guid id)
    {
        return await _context.Wallet.FirstOrDefaultAsync(wallet => wallet.WalletId == id);
    }

    public async Task<Wallet> Create(Wallet wallet)
    {
        await _context.Wallet.AddAsync(wallet);
        await _context.SaveChangesAsync();
        
        return wallet;
    }

    public async Task<Wallet> Update(Wallet wallet)
    {
        _context.Wallet.Update(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }
}