namespace Assessment.Models.ViewModel;

public class OrderDetailsViewModel
{
    
    public int Id { get; set; }
    public string OrderStatus { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public List<ProductViewModal>? OrderedProducts { get; set; }
}
