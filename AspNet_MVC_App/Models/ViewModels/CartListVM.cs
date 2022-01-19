using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Models.ViewModels
{
    public class CartListVM
    {
        public IEnumerable<Car> Cars { get; set; }
    }
}
