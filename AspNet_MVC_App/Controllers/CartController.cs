using AspNet_MVC_App.Data;
using AspNet_MVC_App.Models;
using AspNet_MVC_App.Models.ViewModels;
using AspNet_MVC_App.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private SalonDbContext _context;

        public CartController(SalonDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<ShoppingProduct> products = HttpContext.Session.GetObject<List<ShoppingProduct>>(WebConstants.cartKey);
            if (products == null)
                products = new List<ShoppingProduct>();

            int[] productIds = products.Select(i => i.ProductId).ToArray();

            CartListVM viewModel = new CartListVM()
            {
                Cars = _context.Cars.Where(c => productIds.Contains(c.Id))
            };

            return View(viewModel);
        }

        public IActionResult Confirm()
        {
            return View();
        }
    }
}
