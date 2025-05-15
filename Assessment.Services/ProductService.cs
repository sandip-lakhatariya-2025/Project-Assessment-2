using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;
using Assessment.Models.ViewModel;
using Assessment.Services.IServices;

namespace Assessment.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }
    public async Task<(bool isSuccess, string message)> AddProductAsync(ProductViewModal product)
    {
        try
        {
            Product newProduct = new Product{
                ProductName = product.ProductName,
                ProductRate = product.ProductRate,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                Category = product.Category
            };

            bool isAdded = await _unitOfWork.Products.InsertAsync(newProduct);
            
            if(isAdded) {
                return (true, "Product has been added successfully.");
            }

            return (false, "Some error occured.");
        }
        catch (Exception ex)
        {
            return(false, $"There is an error: {ex}");
        }
    }

}
