using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyWebPage.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyWebPage.ViewModels
{
   // [NotMapped]
    public class ProductViewModel
    {
        
        public string Category { get; set; }
     
        public string Size { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }

        public string ArtDescription { get; set; }
        public string ArtDating { get; set; }
      
        public string ArtId { get; set; }
        [Required]
        public string Artist { get; set; }
        public DateTime ArtistBirthDate { get; set; }
        public DateTime ArtistDeathDate { get; set; }
        public string ArtistNationality { get; set; }
    }
}
