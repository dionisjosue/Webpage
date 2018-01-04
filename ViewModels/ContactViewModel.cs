using System.ComponentModel.DataAnnotations;

namespace BuyWebPage.ViewModels
{

    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 15)]
        public string Message { get; set; }
    }
}
