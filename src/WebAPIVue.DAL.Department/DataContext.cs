using Microsoft.EntityFrameworkCore;

namespace WebAPIVue.DAL.Department
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Entities.Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Department>().HasData(
                new Entities.Department() { Id = 1, Title = "Department1" },
                new Entities.Department() { Id = 2, Title = "Department2" });
        }
    }
}
