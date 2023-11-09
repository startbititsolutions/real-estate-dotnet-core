
namespace Danweyne_Real_estate.Models.Mapper
{
    public class ProfileViewModel
    {
        public string? imgurl { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; }
        public string? Address { get; set; }
        public string? googleurl { get; set; }
        public string? twitterurl { get; set; }
        public string? Facebookurl { get; set; }
        public string? linkedinurl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? PhoneNumber {
            get; set;
        }
    }
}
