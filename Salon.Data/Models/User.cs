using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
