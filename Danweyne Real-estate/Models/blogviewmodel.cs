using Models.Database_Models;

namespace Danweyne_Real_estate.Models
{
    public class blogviewmodel
    {
        //public int BlogId { get; set; }
        public string AuthorName { get; set; }

        public string BlogName { get; set; }
        public string Title { get; set; }


        public IFormFile BlogImg { get; set; }
        public string BlogDesc { get; set; }
        public DateTime DateTime { get; set; }
        public Blog Blog { get; set; }
        public string Summary { get; set; }
    }
}
