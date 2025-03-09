using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Data.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Sum).IsRequired();
            builder.Property(x => x.FromCurrBalance).IsRequired();
            builder.Property(x => x.FromPrevBalance).IsRequired();
            builder.Property(x => x.ToCurrBalance).IsRequired();
            builder.Property(x => x.ToPrevBalance).IsRequired();
            builder.Property(x => x.SequenceNumber).UseIdentityColumn();

            builder.HasOne(x => x.AccountFrom).WithMany().HasForeignKey(x => x.AccountFromId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(x => x.AccountTo).WithMany().HasForeignKey(x => x.AccountToId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            
//            builder.HasOne(x => x.AccountTo).WithMany().HasForeignKey(x => x.AccountToId);
        }
    }
}