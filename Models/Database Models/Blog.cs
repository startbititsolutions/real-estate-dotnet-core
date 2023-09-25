using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string AuthorName { get; set; }

        public string BlogName { get; set; }

        public string BlogImg { get; set; }
        public string BlogDesc { get; set; }
        public DateTime DateTime { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
    }
}
