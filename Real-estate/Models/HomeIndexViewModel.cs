/*using Microsoft.EntityFrameworkCore.Metadata.Internal;*/
using Models.Database_Models;

namespace Danweyne_Real_estate.Models
{
    public class HomeIndexViewModel
    {
       public List<Property> Properties { get; set; }
        public List<Banner> Banners { get; set; }
    }
}
