using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApplication_templatemo_487_fitness_F.Models;

namespace WebApplication_templatemo_487_fitness_F.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

      public   DbSet<Trainer> Trainers { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
