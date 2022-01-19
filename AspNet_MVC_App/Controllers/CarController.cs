using AspNet_MVC_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using AspNet_MVC_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNet_MVC_App.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AspNet_MVC_App.Controllers
{
    [Authorize(Roles = WebConstants.adminRole)]
    public class CarController : Controller
    {
        private SalonDbContext _context;
        private IWebHostEnvironment _host;

        public CarController(SalonDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            return View(_context.Cars.Include(nameof(Car.Category)));
        }
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> categories = _context.Categories.Select(i => new SelectListItem()
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            // ViewData
            //ViewData["CategoryList"] = categories;

            // ViewBag
            //ViewBag.CategoryList = categories;

            CarVM viewModel = new CarVM()
            {
                Categories = _context.Categories.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(viewModel);
        }

        private string SaveCarImage(IFormFile img)
        {
            string root = _host.WebRootPath;
            string folder = root + WebConstants.carImagesPath;
            string name = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(img.FileName);

            string fullPath = Path.Combine(folder, name + extension);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                img.CopyTo(fs);
            }

            return name + extension;
        }
        [HttpPost]
        public IActionResult Create(CarVM model)
        {
            if (!ModelState.IsValid) return View();

            var files = HttpContext.Request.Form.Files;

            string fileName = SaveCarImage(files[0]);

            model.Car.Image = fileName;

            _context.Cars.Add(model.Car);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var carToRemove = _context.Cars.Find(id);

            if (carToRemove == null) return NotFound();

            if (carToRemove.Image != null)
            {
                string imagePath = _host.WebRootPath + Path.Combine(WebConstants.carImagesPath, carToRemove.Image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Cars.Remove(carToRemove);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var car = _context.Cars.Find(id);

            if (car == null) return NotFound();

            IEnumerable<SelectListItem> categories = _context.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryList = categories;

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car updatedCar)
        {
            if (!ModelState.IsValid) return View();

            var files = HttpContext.Request.Form.Files;
            var oldCar = _context.Cars.AsNoTracking().FirstOrDefault(c => c.Id == updatedCar.Id);

            if (files.Any())
            {
                if (oldCar.Image != null)
                {
                    string oldCarImagePath = _host.WebRootPath + Path.Combine(WebConstants.carImagesPath, oldCar.Image);
                
                    if (System.IO.File.Exists(oldCarImagePath))
                    {
                        System.IO.File.Delete(oldCarImagePath);
                    }
                }

                string fileName = SaveCarImage(files[0]);

                updatedCar.Image = fileName;
            }
            else
            {
                updatedCar.Image = oldCar.Image;
            }

            _context.Cars.Update(updatedCar);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
