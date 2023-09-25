using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
   
    public class City
    {
        [Key]
      public int CityId { get; set; }
        [Required]
        public string CityName { get; set; } 
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
