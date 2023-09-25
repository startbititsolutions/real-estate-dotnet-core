using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class PropertyImage
    {
        public int PropertyImageId { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyId { get; set; }
        
        public virtual Property Property { get; set; }
    }
}
