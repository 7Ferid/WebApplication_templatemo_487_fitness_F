using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_templatemo_487_fitness_F.Contexts;
using WebApplication_templatemo_487_fitness_F.Helpers;
using WebApplication_templatemo_487_fitness_F.Models;
using WebApplication_templatemo_487_fitness_F.ViewModels.TrainerViewModels;

namespace WebApplication_templatemo_487_fitness_F.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public TrainerController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVM()
            {
              Id=x.Id,
                Name=x.Name,
                ImagePath=x.ImagePath,
                Duties=x.Duties,
                DepartmentName = x.Department.Name

            }).ToListAsync();
            return View(trainers);

            
        }

        public async Task<IActionResult> Create()
        {
            //var trainers = await _context.Departments.ToListAsync();
            //ViewBag.Trainers = trainers;


            await _SendDepartmentWithViewBag();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TrainerCreateVM vm)
        {
            await _SendDepartmentWithViewBag();
            if (!ModelState.IsValid)
                return View(vm);

            var isExistcategory = await _context.Departments.AnyAsync(x => x.Id == vm.DepartmentId);
            if (!isExistcategory)
            {
                ModelState.AddModelError("DepartmentId", "This Department is not found");
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Image's maximum size must be 2 mb");
                return View(vm);
            }
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "You can upload file in only image format ");
                return View(vm);
            }

           
            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Trainer trainer = new()
            {
                Name = vm.Name,
               Duties=vm.Duties,
               DepartmentId=vm.DepartmentId,
                ImagePath = uniqueFileName
            };

            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Trainers.FindAsync(id);
            if (product is null)
                return NotFound();

            _context.Trainers.Remove(product);
            await _context.SaveChangesAsync();

            string deletedImagePath = Path.Combine(_folderPath, product.ImagePath);
            
            FileHelper.FileDelete(deletedImagePath);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Trainers.FindAsync(id);
            if (product is null)
                return NotFound();

            TrainerUpdateVM vm = new()
            {
                Id = product.Id,
                DepartmentId = product.DepartmentId,
                Name = product.Name,
               Duties=product.Duties,
          
            };

            await _SendDepartmentWithViewBag();
            return View(vm);
        }
        [HttpPost]

        public async Task<IActionResult> UpdateAsync(TrainerUpdateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);


            var isExistcategory = await _context.Departments.AnyAsync(x => x.Id == vm.DepartmentId);
            if (!isExistcategory)
            {
                ModelState.AddModelError("DepartmentId", "This Department is not found");
                return View(vm);
            }
            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Image's maximum size must be 2 mb");
                return View(vm);
            }
            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "You can upload file in only image format ");
                return View(vm);
            }

            var existProduct = await _context.Trainers.FindAsync(vm.Id);
            if (existProduct is null)
                return BadRequest();



            existProduct.Name = vm.Name;
            existProduct.Duties = vm.Duties;
            existProduct.DepartmentId = vm.DepartmentId;


            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                string oldImagePath = Path.Combine(_folderPath, existProduct.ImagePath);
                FileHelper.FileDelete(oldImagePath);
                existProduct.ImagePath = newImagePath;
            }

            _context.Trainers.Update(existProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        private async Task _SendDepartmentWithViewBag()
        {
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = departments;
        }


    }
}
