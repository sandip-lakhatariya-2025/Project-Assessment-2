using System.ComponentModel.DataAnnotations;

namespace Assessment.Models.ViewModel;

public class ProductViewModal
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Product name is required.")]
    [RegularExpression(@"^[^\d\s].*", ErrorMessage = "Name cannot start with a number or space.")]
    [StringLength(100)]
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }

    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public decimal ProductRate { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Quantity must be a whole number.")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = null!;
    public int ProductQuantity { get; set; }
}
