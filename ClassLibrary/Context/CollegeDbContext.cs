using ClassLibrary.Configuration;
using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Context
{
    public class CollegeDbContext:DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student>  Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
