using Microsoft.EntityFrameworkCore;
using SimplifiedPicPay.Services;
using SimplifiedPicPay.Context;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Repositories.Interfaces;

namespace SimplifiedPicPay.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Transaction?>> GetAll()
    {
        return await _context.Transaction.ToListAsync();
    }

    public async Task<IEnumerable<Transaction?>> GetByClientWallet(Guid walletId)
    {
        IEnumerable<Transaction?> transactionsByUser = await _context.Transaction.Where(t => t.PayerWalletId == walletId).ToListAsync();
        return transactionsByUser;
    }


    public async Task<Transaction?> GetById(Guid id)
    {
        Transaction? transaction = await _context.Transaction.FirstOrDefaultAsync(t => t != null && t.TransactionId == id);
        return transaction;
    }

    public async Task<Transaction> Create(Transaction transaction)
    {
        await _context.Transaction.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}