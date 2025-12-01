using Microsoft.EntityFrameworkCore;
using ProjetoDevTrail.Api.Models;

namespace ProjetoDevTrail.Infra.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Account_Id);
                entity.Property(a => a.Number)
                      .UseIdentityColumn(seed: 100000, increment: 1)
                      .IsRequired();
                entity.HasIndex(a => a.Number).IsUnique();
                entity.Property(a => a.Balance).IsRequired();
                entity.Property(a => a.Status).HasConversion<string>();
                entity.Property(a => a.Type).HasConversion<string>();

                entity.HasOne<Client>()
                      .WithMany(c => c.Accounts)
                      .HasForeignKey(a => a.Client_Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasDiscriminator<AccountType>("Type")
                      .HasValue<SavingsAccount>(AccountType.Savings)
                      .HasValue<CurrentAccount>(AccountType.Current);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Client_Id);
                entity.HasIndex(c => c.CPF).IsUnique();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.CPF).IsRequired().HasMaxLength(11);
                entity.Property(c => c.DateOfBirth).IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Transaction_Id);
                entity.Property(t => t.Type).HasConversion<string>();

                entity.HasOne(t => t.OriginAccount)
                      .WithMany()
                      .HasForeignKey(t => t.Origin_Account_Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.TargetAccount)
                      .WithMany()
                      .HasForeignKey(t => t.Target_Account_Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
