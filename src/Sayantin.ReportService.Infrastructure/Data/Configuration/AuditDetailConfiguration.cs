using ReportService.Domain.Audit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data.Configuration
{
    public class AuditDetailConfiguration : IEntityTypeConfiguration<AuditDetail>
    {
        public void Configure(EntityTypeBuilder<AuditDetail> builder)
        {
            builder.ToTable("AuditDetail");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PropertyName)
                   .HasMaxLength(200)
                   .IsRequired()
                   .HasComment("نام ویژگی تغییر یافته");

            builder.Property(x => x.OldValue)                   
                   .IsRequired(true).HasComment("مقدار قبلی"); ;

            builder.Property(x => x.NewValue)                  
                   .IsRequired(true).HasComment("مقدار جدید"); ;

            // Relationship with AuditMaster (many-to-one)
            builder.HasOne(x => x.AuditMaster)
                   .WithMany(am => am.AuditDetails)
                   .HasForeignKey(x => x.AuditMasterId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: EF uses private backing field for navigation
            builder.Metadata
                   .FindNavigation(nameof(AuditDetail.AuditMaster))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }

}
