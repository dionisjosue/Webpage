using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuyWebPage.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20,MinimumLength =3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }


    }
}
