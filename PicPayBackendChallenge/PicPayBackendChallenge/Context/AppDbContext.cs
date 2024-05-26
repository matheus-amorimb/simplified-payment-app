using Microsoft.EntityFrameworkCore;
using PicPayBackendChallenge.Models;

namespace PicPayBackendChallenge.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<Wallet>? Wallet { get; set; }
}