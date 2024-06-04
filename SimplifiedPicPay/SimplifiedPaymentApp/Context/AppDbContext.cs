using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimplifiedPicPay.Models;

namespace SimplifiedPicPay.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
            .HasOne(w => w.User)
            .WithOne(u => u.Wallet)
            .HasForeignKey<Wallet>(w => w.UserId);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Cpf)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
    
    
}