namespace Data.EntityConfigurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyEntity>
    {
        public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
        {
            builder.HasKey(entity => new { entity.CurrencyId });
            builder.Property(f => f.CurrencyId).ValueGeneratedOnAdd();
            builder.HasMany(f => f.Branches);
        }
    }
}
