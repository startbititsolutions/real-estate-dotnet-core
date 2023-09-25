using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class FaqQue
    {

        [Key]
        public int que_id { get; set; }
        [Required]
        public string que { get; set; }
        [ForeignKey("que_id")]
        public virtual ICollection<FaqAns> FaqAns { get; set; }
    }
}
