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

namespace AspNet_MVC_App.Controllers
{
    public class CarController : Controller
    {
        private SalonDbContext _context;

        public CarController(SalonDbContext context)
        {
            _context = context;
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

        [HttpPost]
        public IActionResult Create(CarVM model)
        {
            if (!ModelState.IsValid) return View();

            _context.Cars.Add(model.Car);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var carToRemove = _context.Cars.Find(id);

            if (carToRemove == null) return NotFound();

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
        public IActionResult Edit(Car obj)
        {
            if (!ModelState.IsValid) return View();

            _context.Cars.Update(obj);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
