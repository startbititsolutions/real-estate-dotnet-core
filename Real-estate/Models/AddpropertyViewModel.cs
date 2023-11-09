using Models;
using Models.Database_Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Danweyne_Real_estate.Models
{
    public class AddpropertyViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string Catagory { get; set; }
        /*public State State { get; set; }
        public City City { get; set; }*/
        /*public Status Status { get; set; }*/
        public int Garage { get; set; }
        public string Baths { get; set; }
        public string Beds { get; set; }
        public string Area { get; set; }
        public IFormFile File { get; set; }
        public List<IFormFile> MultiFiles { get; set; }
        public string CoverImageUrl { get; set; }
        public Features Features { get; set; }
        public IEnumerable<Image>? Image { get; set; }
        public AdditionalDetails AdditionalDetails { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL.")]
        [Required(ErrorMessage = "The VidUrl field is required.")]
        [RegularExpression(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$", ErrorMessage = "Invalid video URL")]
        public string VidUrl { get; set; }
        public bool TermsAndCondition { get; set; }
    }
}
