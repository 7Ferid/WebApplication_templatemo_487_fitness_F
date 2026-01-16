using System.ComponentModel.DataAnnotations;

namespace WebApplication_templatemo_487_fitness_F.ViewModels.TrainerViewModels
{
    public class TrainerUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string Duties { get; set; } = string.Empty;
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
