namespace Data.EntityConfigurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BranchConfiguration : IEntityTypeConfiguration<BranchEntity>
    {
        public void Configure(EntityTypeBuilder<BranchEntity> builder)
        {
            builder.HasKey(entity => new { entity.BranchId });
            builder.Property(f => f.BranchId).ValueGeneratedOnAdd();
        }
    }
}
