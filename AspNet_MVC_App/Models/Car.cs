using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AspNet_MVC_App.Models
{
    public class Car
    {
        public string Model { get; set; }
        public string Color { get; set; }
        [DisplayName("Manufacture Date")]
        public DateTime ManufactureDate { get; set; }
    }
}
