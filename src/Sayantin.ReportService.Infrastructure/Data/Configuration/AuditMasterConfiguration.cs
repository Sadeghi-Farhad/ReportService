using ReportService.Domain.Audit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data.Configuration
{
    public class AuditMasterConfiguration : IEntityTypeConfiguration<AuditMaster>
    {
        public void Configure(EntityTypeBuilder<AuditMaster> builder)
        {
            builder.ToTable("AuditMaster");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ParentId).HasComment("شناسه Parent Entity که میخواهیم آدیت کنیم").IsRequired();
            builder.Property(x => x.EntityId).HasComment("شناسه entity که میخواهیم آدیت کنیم").IsRequired();
            builder.Property(x => x.EntityName).HasComment("نام Entity که میخواهیم آدیت کنیم")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Action).HasComment("نوع عملیاتی که انجام شده")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.PrsCode).HasComment("کد پرسنلی فردی که تغییرات داده").IsRequired();

            builder.Property(x => x.TimeStamp).HasComment("زمان انجام تغییرات")
                   .IsRequired();

            // Relationship: AuditMaster 1 -> many AuditDetails
            builder.HasMany(x => x.AuditDetails)
                   .WithOne()
                   .HasForeignKey("AuditMasterId")           // shadow FK column
                   .OnDelete(DeleteBehavior.Cascade);

            // EF must use the private backing field for the list
            builder.Metadata
                   .FindNavigation(nameof(AuditMaster.AuditDetails))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }

}
