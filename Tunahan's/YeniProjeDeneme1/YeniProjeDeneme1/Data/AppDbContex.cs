using Microsoft.EntityFrameworkCore;
using YeniProjeDeneme1.Entities;

namespace YeniProjeDeneme1.Data
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options){}
        public DbSet<Entities.Sensor_Data> Sensor_Data { get; set; }
        public DbSet<Entities.Sensor> Sensor { get; set; }
        public DbSet<Entities.Product> Product { get; set; }
        public DbSet<Entities.User> User { get; set; }
        public DbSet<Entities.Project> Project { get; set; }
        public DbSet<Entities.ProjectProduct> ProjectProduct { get; set; }
        public DbSet<Entities.ProjectUser> ProjectUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Sensor_Data>()
                .HasOne(p => p.Sensor)
                .WithMany(p => p.Sensor_Datas)
                .HasForeignKey(p => p.SensorId);
            modelBuilder.Entity<Entities.Sensor>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Sensor)
                .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<ProjectProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProjectProducts)
                .HasForeignKey(pp => pp.ProductId);
            modelBuilder.Entity<ProjectProduct>()
                .HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectProducts)
                .HasForeignKey(pp => pp.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(p=> p.ProjectUsers)
                .HasForeignKey(pu => pu.UserId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId);
        }
    }
}
