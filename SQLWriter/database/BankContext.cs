using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SQLWriter.Models;

namespace SQLWriter.Database;

public class BankContext : DbContext
{
    public DbSet<Bank> Banks {get;set;}
    public DbSet<User> Users {get;set;}
    public DbSet<BankAccount> BankAccounts {get;set;}
    public DbSet<BankAccountUser> BankAccountUsers {get;set;}
    

    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(@"Server=tcp:sql-cloudnative-002.database.windows.net,1433;Initial Catalog=SqlDb-cloudnative-002;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication='Active Directory Default';");
        
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>().Property(b => b.Id).ValueGeneratedNever();
        modelBuilder.Entity<BankAccount>().Property(b => b.Id).ValueGeneratedNever();
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedNever();

        modelBuilder.Entity<Bank>()
            .HasMany(e => e.BankAccounts)
            .WithOne(e => e.Bank)
            .HasForeignKey(e => e.BankId);
        
        modelBuilder.Entity<Bank>()
            .HasMany(e => e.Users)
            .WithOne(e => e.Bank)
            .HasForeignKey(e => e.BankId);

        modelBuilder.Entity<BankAccount>()
            .HasMany(e => e.Users)
            .WithMany(e => e.BankAccounts)
            .UsingEntity<BankAccountUser>();
    }
}
