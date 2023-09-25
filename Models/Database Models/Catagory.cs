using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Catagory
    {
        [Key]
        public int CatgoryId { get; set; }
        [Required]
        public string? CatgoryName { get; set; }
   

    }
}
