using System.ComponentModel.DataAnnotations;

namespace BuyWebPage.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
