using ReportService.Domain.Audit;
using ReportService.Domain.Base;
using ReportService.Domain.Blogs;
using ReportService.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Infrastructure.Data.Configuration
{
    public class EFDbContext(DbContextOptions<EFDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditMaster> AuditMaster { get; set; }
        public DbSet<AuditDetail> AuditDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEntity>();
            modelBuilder.Ignore<BaseEntity<int>>();
            modelBuilder.Ignore<BaseDomainEvent>();

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Id)
                .HasComment("شناسه بلاگ")
                .IsRequired();

                entity.Property(b => b.AuthorId)
                .HasComment("شناسه نویسنده")
                .IsRequired();

                entity.Property(b => b.Title)
                .HasComment("عنوان بلاگ")
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(b => b.Description)
                .HasComment("متن بلاگ")
                .HasMaxLength(500);

                entity.Property(b => b.IsPublished)
                .HasComment("وضعیت انتشار");

                entity.Property(b => b.IsDelivered)
               .HasComment("وضعیت تحویل");

                // Relationships
                entity.HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(e => e.Id)
                .HasComment("شناسه کاربر")
                .IsRequired();

                entity.Property(u => u.Name)
                .HasComment("نام کاربر")
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(u => u.Email)
                .HasComment("ایمیل کاربر")
                .HasMaxLength(100);

                entity.Property(u => u.Birthday)
                .HasComment("تاریخ تولد کاربر");

                // Relationships
                entity.HasMany(u => u.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

                var x = entity.OwnsOne(u => u.Address);
                x.Property(u => u.Province).HasComment("استان");
                x.Property(u => u.City).HasComment("شهر");
                x.Property(u => u.Street).HasComment("خیابان");
                x.Property(u => u.PostalCode).HasComment("کد پستی");
            });           

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuditMasterConfiguration());
            modelBuilder.ApplyConfiguration(new AuditDetailConfiguration());
        }
    }
}