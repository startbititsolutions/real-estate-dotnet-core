using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Banner
    {
        [Key]
        public int BannerId { get; set; }
        public string BannerName { get; set;}
        [Required]
        public string BannerUrl { get; set; }
    }
}
