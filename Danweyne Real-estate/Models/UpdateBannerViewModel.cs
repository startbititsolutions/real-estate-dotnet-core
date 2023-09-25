namespace Danweyne_Real_estate.Models
{
    public class UpdateBannerViewModel
    {
        public int BannerId { get; set; }

        public string BannerName { get; set; }
        public string? OldImage { get; set; }

        public IFormFile? NewImage { get; set; }
    }
}
