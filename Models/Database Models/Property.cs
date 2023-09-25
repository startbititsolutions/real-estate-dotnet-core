using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        [Required]
        public string PropertyName { get; set; }
        public string CoverImageUrl { get; set; }
        public int CatagoryId { get; set; }
        
        public virtual Catagory Catagory { get; set; }
        public int PropertyPrice { get; set; }
        public string PropertyDescription { get; set; }
        public  string PropertyVideo { get; set; }

        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        // public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PropertyImage>? Images { get; set; }
        public DateTime DateTime { get; set; }
        public int StatusId { get; set; }
        
        public virtual Status? Status { get; set; }
        public int Area { get; set; }
        public int BedRooms { get; set; }
        public int? Garage { get; set; }
        public int BathRooms { get; set; }
       public int CityId { get; set; }
        public virtual City? City { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
       public bool IsActive { get; set; }
    }
}
