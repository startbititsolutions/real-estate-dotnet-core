using System.ComponentModel.DataAnnotations;

namespace Danweyne_Real_estate.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
