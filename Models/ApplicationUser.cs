using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Discription { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public string? googleurl { get; set; }
        public string? twitterurl { get; set; }
        public string? Facebookurl { get; set; }
        public string? linkedinurl { get; set; }
        public string? InstagramUrl { get; set;}
    }
}
