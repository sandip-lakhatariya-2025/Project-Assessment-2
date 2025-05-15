using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public string OrderStatus { get; set; } = null!;
    
    [ForeignKey("User")]
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
