using Microsoft.EntityFrameworkCore;

namespace WebAPIVue.DAL.User
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>().HasData(
                new Entities.User() { Id = 1, UserName = "User1", DepartmentId = 2 },
                new Entities.User() { Id = 2, UserName = "User2", DepartmentId = 1 });
        }
    }
}
