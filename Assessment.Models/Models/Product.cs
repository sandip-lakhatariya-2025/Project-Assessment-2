namespace Assessment.Models.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal ProductRate { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = null!;
    public bool IsDeleted { get; set; }
}
