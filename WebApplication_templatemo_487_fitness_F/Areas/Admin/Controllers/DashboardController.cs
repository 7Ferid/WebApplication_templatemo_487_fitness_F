using Microsoft.AspNetCore.Mvc;

namespace WebApplication_templatemo_487_fitness_F.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
