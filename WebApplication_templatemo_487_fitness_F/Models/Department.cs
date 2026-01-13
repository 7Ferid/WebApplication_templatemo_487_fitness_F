using WebApplication_templatemo_487_fitness_F.Models.Common;

namespace WebApplication_templatemo_487_fitness_F.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Trainer> Trainers { get; set; } = [];
    }
}
