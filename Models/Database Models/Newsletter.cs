
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Newsletter
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Email id is required")]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", 
        //    ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        public string Email{ get; set; }
    }
}
