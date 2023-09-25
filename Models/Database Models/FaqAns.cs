using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class FaqAns
    {
        [Key]
        public int ans_id { get; set; }
        [Required]
        public string ans { get; set; }
        public int que_id { get; set; }
        public virtual FaqQue FaqQue { get; set; }
    }
}
