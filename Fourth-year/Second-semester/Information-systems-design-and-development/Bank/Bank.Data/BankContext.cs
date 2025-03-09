using Bank.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    internal class BankContext : DbContext
    {
        public BankContext() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=(local);Initial Catalog=Bank;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new DepositConfiguration());
            modelBuilder.ApplyConfiguration(new DepositInfoConfiguration());
            modelBuilder.ApplyConfiguration(new CreditConfiguration());
            modelBuilder.ApplyConfiguration(new CreditInfoConfiguration());
        }
    }
}
