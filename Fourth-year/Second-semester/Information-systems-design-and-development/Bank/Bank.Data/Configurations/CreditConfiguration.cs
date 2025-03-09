using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Data.Configurations
{
    internal sealed class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.ToTable(nameof(Credit));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Sum).IsRequired();
            builder.Property(x => x.Finished).IsRequired();

            //builder.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId);
            builder.HasOne(x => x.MainAccount).WithMany().HasForeignKey(x => x.MainAccountId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.PersentAccount).WithMany().HasForeignKey(x => x.PersentAccountId).OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(x => x.PersentAccount).WithMany().HasForeignKey(x => x.PersentAccountId);
        }
    }
}