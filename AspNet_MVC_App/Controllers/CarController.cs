using AspNet_MVC_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace AspNet_MVC_App.Controllers
{
    public class CarController : Controller
    {
        static List<Car> CarList = new List<Car>();

        static CarController()
        {
            CarList.Add(new Car() { Id=1, Model = "VW Passat B2", Color = "Red", ManufactureDate = new DateTime(1987, 12, 15) });
            CarList.Add(new Car() { Id=2, Model = "Nissan Juke", Color = "White", ManufactureDate = new DateTime(2010, 08, 5) });
            CarList.Add(new Car() { Id=3, Model = "VW Passat B3", Color = "Bordeux", ManufactureDate = new DateTime(1995, 9, 11) });
        }

        public IActionResult Index()
        {
            return View(CarList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car newCar)
        {
            if (!ModelState.IsValid) return View();

            CarList.Add(newCar);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            CarList.Remove(CarList.First(c => c.Id == id));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var car = CarList.First(c => c.Id == id);
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car obj)
        {
            if (!ModelState.IsValid) return View();

            var car = CarList.First(c => c.Id == obj.Id);
            car.Model = obj.Model;
            car.Color = obj.Color;
            car.ManufactureDate = obj.ManufactureDate;

            return RedirectToAction(nameof(Index));
        }
    }
}
