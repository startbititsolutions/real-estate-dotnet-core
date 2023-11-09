using System.ComponentModel.DataAnnotations;

namespace Danweyne_Real_estate.Models
{

    public class UpdateBlogViewModel
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Author Name is required.")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Blog Name is required.")]
        public string BlogName { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Blog Description is required.")]
        public string BlogDesc { get; set; }

        [Required(ErrorMessage = "Summary is required.")]
        public string Summary { get; set; }

        public string? OldImage { get; set; }

        public IFormFile? NewImage { get; set; }
    }

}

