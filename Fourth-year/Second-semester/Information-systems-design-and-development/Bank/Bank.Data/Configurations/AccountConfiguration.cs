using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Data.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Deleted).IsRequired();
            builder.Property(x => x.Debet).IsRequired();
            builder.Property(x => x.Credit).IsRequired();
            builder.Property(x => x.Passive).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            //builder.Property(x => x.Balance).HasComputedColumnSql("[]")


            builder.HasOne(x => x.AccountType).WithMany().HasForeignKey(x => x.AccountTypeId);
            builder.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId);
        }
    }
}
