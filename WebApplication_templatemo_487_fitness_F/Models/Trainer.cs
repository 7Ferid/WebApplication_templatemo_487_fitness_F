using WebApplication_templatemo_487_fitness_F.Models.Common;

namespace WebApplication_templatemo_487_fitness_F.Models
{
    public class Trainer:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Duties { get; set; } = string.Empty;
        
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
