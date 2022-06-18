using Demo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Persistence.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("RX_Job");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.DelayReason).HasMaxLength(50);

            builder.HasOne(p => p.RoomType)
                .WithMany(p => p.Jobs)
                .HasForeignKey(p => p.RoomTypeId);
        }
    }
}
