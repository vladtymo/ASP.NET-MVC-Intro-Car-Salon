using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Models
{
    public class OrderSummaryMail
    {
        public IEnumerable<Car> Cars { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }
    }
}
