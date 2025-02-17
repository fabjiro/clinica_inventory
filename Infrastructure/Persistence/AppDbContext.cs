using Domain.Const;
using Domain.Entities;
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
    public virtual DbSet<CivilStatusEntity> CivilStatusEntities { get; set; }
    public virtual DbSet<PatientEntity> PatientEntity { get; set; }
    public virtual DbSet<GroupEntity> GroupEntity { get; set; }
    public virtual DbSet<ExamEntity> ExamEntity { get; set; }
    public virtual DbSet<ConsultEntity> ConsultEntity { get; set; }
    public virtual DbSet<SubRolEntity> SubRolEntity { get; set; }
    public virtual DbSet<PageEntity> PageEntity { get; set; }
    public virtual DbSet<PagePermitsEntity> PagePermitsEntity { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Default", b => b.MigrationsAssembly("Infrastructure"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Avatar)
            .WithMany()
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.Restrict);
        
        

        modelBuilder.Entity<UserEntity>().Property(e => e.AvatarId).IsRequired().HasDefaultValue(Guid.Parse(DefaulConst.DefaultAvatarUserId));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
