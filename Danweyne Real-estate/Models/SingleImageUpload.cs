using System.ComponentModel.DataAnnotations;

namespace Danweyne_Real_estate.Models
{
    public class SingleFileModel 
    {
       
        public string? FileName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
    }
}
