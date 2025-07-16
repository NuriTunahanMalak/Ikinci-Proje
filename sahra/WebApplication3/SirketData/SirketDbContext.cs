using Microsoft.EntityFrameworkCore;
using SirketEntites;

namespace SirketData
{
    public class SirketDbContext : DbContext
    {
        public SirketDbContext(DbContextOptions<SirketDbContext> options) : base(options)
        {
        }

        // Tablolar
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<ProjectProduct> ProjectProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key: UserProject
            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

            // Composite key: ProjectProduct
            modelBuilder.Entity<ProjectProduct>()
                .HasKey(pp => new { pp.ProjectId, pp.ProductId });

            modelBuilder.Entity<ProjectProduct>()
                .HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectProducts)
                .HasForeignKey(pp => pp.ProjectId);

            modelBuilder.Entity<ProjectProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProjectProducts)
                .HasForeignKey(pp => pp.ProductId);

            // User-Department ilişkisi
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
