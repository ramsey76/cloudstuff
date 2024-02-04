using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SQLWriter.Models;

namespace SQLWriter.Database;

public class BankContext : DbContext
{
    public DbSet<User> Users {get;set;}
    public DbSet<Bank> Banks {get;set;}
    public DbSet<BankAccount>BankAccounts{get;set;}
    public DbSet<BankAccountUser> BankAccountUsers {get;set;}
    

    // public BankContext(DbContextOptions<BankContext> options) : base(options)
    // {
    // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sql-cloudnative-002.database.windows.net;Database=SqlDb-cloudnative-002;User Id=mainuser;Password=Marvel0!");
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>()
        .HasOne<Bank>(b => b.Bank)
        .WithMany(b => b.BankAccounts)
        .HasForeignKey(b => b.BankId);

        modelBuilder.Entity<BankAccountUser>()
        .HasOne<BankAccount>(bau => bau.BankAccount)
        .WithMany(ba => ba.BankAccountUsers)
        .HasForeignKey(bau => bau.BankAccountId);

        modelBuilder.Entity<BankAccountUser>()
        .HasOne<User>(bau => bau.User)
        .WithMany(u => u.BankAccountUsers)
        .HasForeignKey(bau => bau.UserId);
        

        base.OnModelCreating(modelBuilder);
    }
}
