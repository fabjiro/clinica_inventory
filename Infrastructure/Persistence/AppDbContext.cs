using Domain.Const;
using Domain.Entities;
using Domain.Entities.Shop;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<RolEntity> RolEntity { get; set; }
    public virtual DbSet<UserEntity> UserEntity { get; set; }
    public virtual DbSet<ImageEntity> ImageEntity { get; set; }
    public virtual DbSet<BackupEntity> BackupEntity { get; set; }
    public virtual DbSet<ShopTypeEntity> ShopTypeEntities { get; set; }
    public virtual DbSet<ShopEntity> ShopEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Default", b => b.MigrationsAssembly("Infrastructure"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Avatar)
            .WithMany()
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ShopEntity>().Property(e => e.LogoId).IsRequired().HasDefaultValue(Guid.Parse(DefaulConst.DefaultImageShop));


        modelBuilder.Entity<UserEntity>().Property(e => e.AvatarId).IsRequired().HasDefaultValue(Guid.Parse(DefaulConst.DefaultAvatarUserId));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}