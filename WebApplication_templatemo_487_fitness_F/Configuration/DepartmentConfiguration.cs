using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication_templatemo_487_fitness_F.Models;

namespace WebApplication_templatemo_487_fitness_F.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        }
    }
}
