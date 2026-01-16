using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_templatemo_487_fitness_F.Contexts;
using WebApplication_templatemo_487_fitness_F.ViewModels.TrainerViewModels;


namespace WebApplication_templatemo_487_fitness_F.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
      public async Task<IActionResult> Index()
        {
            var trainers = await  _context.Trainers.Select(x => new TrainerGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Duties = x.Duties,
                DepartmentName = x.Department.Name

            }).ToListAsync();
            return View(trainers);
        }
    }
}
