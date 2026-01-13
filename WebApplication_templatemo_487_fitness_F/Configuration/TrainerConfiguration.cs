using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication_templatemo_487_fitness_F.Models;

namespace WebApplication_templatemo_487_fitness_F.Configuration
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.Duties).IsRequired().HasMaxLength(1024);


            builder.HasOne(x => x.Department).WithMany(x => x.Trainers).HasForeignKey(x => x.DepartmentId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);


        }
        
    }
}
