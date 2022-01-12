using AspNet_MVC_App.Data;
using AspNet_MVC_App.Models;
using AspNet_MVC_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalonDbContext _context;

        public HomeController(SalonDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM viewModel = new HomeVM()
            {
                Cars = _context.Cars.Include(nameof(Car.Category))
            };

            return View(viewModel);
        }

        public IActionResult CarDetails(int id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null) return NotFound();

            _context.Entry(car).Reference(nameof(Car.Category)).Load();

            return View(car);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()    
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
