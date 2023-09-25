using Models;
using Models.Database_Models;

namespace Danweyne_Real_estate.Models
{
    public class propertymodelview
    {
        public Property? Property { get; set; }
        public Features? Features { get; set; }
        public AdditionalDetails? AdditionalDetails { get; set; }
        public List<UserViewModel> Users { get; set; }



    }
}
