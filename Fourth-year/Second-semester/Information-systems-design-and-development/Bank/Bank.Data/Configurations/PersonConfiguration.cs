using Bank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Data.Configurations
{
    internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(nameof(Person));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Patronymic).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.SeriaPassport).IsRequired();
            builder.Property(x => x.PassportNumber).IsRequired();
            builder.Property(x => x.KemVidan).IsRequired();
            builder.Property(x => x.DateVidan).IsRequired();
            builder.Property(x => x.IdentNumber).IsRequired();
            builder.Property(x => x.BirthPlace).IsRequired();
            builder.Property(x => x.CityId);
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.HomePhone);
            builder.Property(x => x.MobPhone);
            builder.Property(x => x.Email);
            builder.Property(x => x.WorkPlace);
            builder.Property(x => x.Position);
            builder.Property(x => x.FamilyStatus).IsRequired();
            builder.Property(x => x.Nationality).IsRequired();
            builder.Property(x => x.Disability).IsRequired();
            builder.Property(x => x.Pensioner).IsRequired();
            builder.Property(x => x.Salary);
            builder.Property(x => x.Army).IsRequired();


            builder.HasIndex(x => new { x.SeriaPassport, x.PassportNumber }).IsUnique();
            builder.HasIndex(x => x.IdentNumber).IsUnique();

            builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId);
        }
    }
}