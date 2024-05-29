using Microsoft.EntityFrameworkCore;
using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<Wallet>? Wallet { get; set; }
    public DbSet<Transaction?> Transaction { get; set; }
    public DbSet<WalletType> WalletType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WalletType>().HasData(
            new WalletType {Id = 1, Description = "User"},
            new WalletType {Id = 2, Description = "Merchant"}
        );

        modelBuilder.Entity<Wallet>()
            .HasIndex(e => e.Email)
            .IsUnique();       
        
        modelBuilder.Entity<Wallet>()
            .HasIndex(e => e.Cpf)
            .IsUnique();
    }
}