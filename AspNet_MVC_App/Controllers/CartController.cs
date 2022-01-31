using AspNet_MVC_App.Data;
using AspNet_MVC_App.Models;
using AspNet_MVC_App.Models.ViewModels;
using AspNet_MVC_App.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salon.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        //private SalonDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IViewRender _viewRender;

        //[BindProperty]
        //private IEnumerable<Car> Cars { get; set; }

        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender, IViewRender viewRender)
        {
            //_context = context;
            _emailSender = emailSender;
            _viewRender = viewRender;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //List<ShoppingProduct> products = HttpContext.Session.GetObject<List<ShoppingProduct>>(WebConstants.cartKey);
            //if (products == null)
            //    products = new List<ShoppingProduct>();

            //int[] productIds = products.Select(i => i.ProductId).ToArray();

            CartListVM viewModel = new CartListVM()
            {
                Cars = GetCarsFromSession()//_context.Cars.Where(c => productIds.Contains(c.Id))
            };

            return View(viewModel);
        }

        private IEnumerable<Car> GetCarsFromSession()
        {
            List<ShoppingProduct> products = HttpContext.Session.GetObject<List<ShoppingProduct>>(WebConstants.cartKey);
            if (products == null)
                products = new List<ShoppingProduct>();

            int[] productIds = products.Select(i => i.ProductId).ToArray();

            return _unitOfWork.CarRepository.Get(c => productIds.Contains(c.Id)); //_context.Cars.Where(c => productIds.Contains(c.Id));
        }

        public IActionResult Confirm()
        {
            string userEmail = User.Identity.Name;
            var items = GetCarsFromSession();

            var html = this._viewRender.Render("Mails/OrderSummary", new OrderSummaryMail 
            {
                UserName = userEmail,
                Cars = items,
                TotalPrice = items.Sum(i => i.Price).Value
            });

            _emailSender.SendEmailAsync(userEmail, "Order Summary", html);

            return View();
        }
    }
}