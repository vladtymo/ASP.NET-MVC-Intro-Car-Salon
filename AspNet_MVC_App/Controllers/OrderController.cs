using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Controllers
{
    [Authorize(Roles = WebConstants.adminRole)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrderList()
        {
            return new JsonResult(new { data = _unitOfWork.CarRepository.Get() });
        }
    }
}
