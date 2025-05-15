namespace Assessment.Models.ViewModel;

public class ProductViewModal
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal ProductRate { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = null!;
    public int ProductQuantity { get; set; }
}
