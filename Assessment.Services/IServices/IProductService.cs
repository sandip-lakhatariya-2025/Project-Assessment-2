using Assessment.Models.ViewModel;

namespace Assessment.Services.IServices;

public interface IProductService
{
    Task<(bool isSuccess, string message)> AddProductAsync(ProductViewModal product);
    Task<(bool isSuccess, string message)> EditProductAsync(ProductViewModal product);
    Task<List<ProductViewModal>?> GetProductList();
    Task<ProductViewModal?> GetProductById(int productId);
    Task<(bool isSuccess, string message)> DeleteProductAsync(int productId);
}
