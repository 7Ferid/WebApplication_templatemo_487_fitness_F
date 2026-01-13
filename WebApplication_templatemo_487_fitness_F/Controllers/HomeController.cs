using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication_templatemo_487_fitness_F.Controllers
{
    public class HomeController : Controller
    {
      public IActionResult Index()
        {
            return View();
        }
    }
}
