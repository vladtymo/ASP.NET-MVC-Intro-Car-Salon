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
            CarList.Add(new Car() { Model = "VW Passat B2", Color = "Red", ManufactureDate = new DateTime(1987, 12, 15) });
            CarList.Add(new Car() { Model = "Nissan Juke", Color = "White", ManufactureDate = new DateTime(2010, 08, 5) });
            CarList.Add(new Car() { Model = "VW Passat B3", Color = "Bordeux", ManufactureDate = new DateTime(1995, 9, 11) });
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
            CarList.Add(newCar);
            return View();
        }
    }
}
