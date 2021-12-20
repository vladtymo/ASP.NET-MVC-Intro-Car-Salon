﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AspNet_MVC_App.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must enter a model")]
        [MinLength(3, ErrorMessage = "Must have at least three symbols")]
        public string Model { get; set; }
        [Required(ErrorMessage = "You must enter a color")]
        public string Color { get; set; }
        [DisplayName("Manufacture Date")]
        public DateTime ManufactureDate { get; set; }
    }
}
