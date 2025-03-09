using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Data.Configurations
{
    internal sealed class DepositInfoConfiguration : IEntityTypeConfiguration<DepositInfo>
    {
        public void Configure(EntityTypeBuilder<DepositInfo> builder)
        {
            builder.ToTable(nameof(DepositInfo));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Revocable).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Persent).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.MinSum).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
        }
    }
}
