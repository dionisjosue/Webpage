using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyWebPage.Data.Entities
{
  public class OrderItem
  {
    [Key]
    public int OrderItemId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    //[ForeignKey("Orderid")]
    public Order order { get; set; }
    //public int Subtotal { get; set; }
    //public int Orderid { get; set; }
  }
}