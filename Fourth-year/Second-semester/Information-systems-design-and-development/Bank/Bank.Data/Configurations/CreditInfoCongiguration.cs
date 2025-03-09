using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Data.Configurations
{
    internal sealed class CreditInfoConfiguration : IEntityTypeConfiguration<CreditInfo>
    {
        public void Configure(EntityTypeBuilder<CreditInfo> builder)
        {
            builder.ToTable(nameof(CreditInfo));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Differantal).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Persent).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.MinSum).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
        }
    }
}
