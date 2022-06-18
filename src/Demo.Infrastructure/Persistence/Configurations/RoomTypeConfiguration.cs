using Demo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Persistence.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("RX_RoomType");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(28);
            builder.Property(p => p.Description).HasMaxLength(255);
        }
    }
}
