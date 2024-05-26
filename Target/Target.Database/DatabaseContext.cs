using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Target.Database;
using Target.Database.Models;

namespace Target.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Server=tcp:sql-cloudnative-002.database.windows.net,1433;Initial Catalog=SqlDb-cloudnative-003;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication='Active Directory Default';");
        // }

        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Depositor> Depositors { get; set; }
        public DbSet<DepositorAccount> DepositorAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Accounts)
                .WithOne(a => a.Institution)
                .HasForeignKey(a => a.InstitutionId);

            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Depositors)
                .WithOne(d => d.Institution)
                .HasForeignKey(d => d.InstitutionId);

            modelBuilder.Entity<Depositor>()
            .HasMany(d => d.Accounts)
            .WithMany(a => a.Depositors)
            .UsingEntity<DepositorAccount>(j => j.HasOne(da => da.Account).WithMany(a => a.DepositorAccounts).HasForeignKey(da => da.AccountId).OnDelete(DeleteBehavior.ClientSetNull),
                j => j.HasOne(da => da.Depositor).WithMany(d => d.DepositorAccounts).HasForeignKey(da => da.DepositorId),
                j => j.HasKey(da => new { da.AccountId, da.DepositorId }));

        }
    }
}

