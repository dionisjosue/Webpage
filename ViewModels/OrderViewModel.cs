using BuyWebPage.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyWebPage.ViewModels
{
   // [NotMapped]
    public class OrderViewModel
    {

        [Required]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        //[StringLength(8,MinimumLength =3)]
        public string OrderNumber { get; set; }
    
        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
