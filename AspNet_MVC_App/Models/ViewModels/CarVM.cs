using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Models.ViewModels
{
    public class CarVM
    {
        public Car Car { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
