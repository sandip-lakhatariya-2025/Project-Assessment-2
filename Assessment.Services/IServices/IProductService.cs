using Assessment.Models.ViewModel;

namespace Assessment.Services.IServices;

public interface IProductService
{
    Task<(bool isSuccess, string message)> AddProductAsync(ProductViewModal product);

    Task<List<ProductViewModal>?> GetProductList();
}
