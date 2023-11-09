using System.ComponentModel.DataAnnotations;

namespace Danweyne_Real_estate.Models
{
    public class MultiFileUpload
    {
        [Required(ErrorMessage = "Please select files")]
        public List<IFormFile> Files { get; set; }
    }
}
