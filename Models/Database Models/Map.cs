using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Map
    {
        [Key]
        public int MapId { get; set; }
        public string Name { get; set; }
    }
}
