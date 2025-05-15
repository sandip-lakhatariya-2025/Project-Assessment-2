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

            bool isExist = await _unitOfWork.Products.ExistAsync(p => 
                !p.IsDeleted &&
                p.ProductName == product.ProductName && 
                p.Category == product.Category
            );

            if(isExist) {
                return (false, $"{product.ProductName} is already exist in {product.Category}.");
            }

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

    public async Task<(bool isSuccess, string message)> EditProductAsync(ProductViewModal product)
    {
        try
        {

            bool isExist = await _unitOfWork.Products.ExistAsync(p => 
                p.Id != product.Id && 
                !p.IsDeleted &&
                p.ProductName == product.ProductName && 
                p.Category == product.Category
            );

            if(isExist) {
                return (false, $"{product.ProductName} is already exist in {product.Category}.");
            }

            Product? existingProduct = await _unitOfWork.Products.GetFirstOrDefault(p => p.Id == product.Id && !p.IsDeleted);

            if(existingProduct != null) {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductRate = product.ProductRate;
                existingProduct.Description = product.Description;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.Category = product.Category;

                bool isUpdated = await _unitOfWork.Products.UpdateAsync(existingProduct);
                
                if(isUpdated) {
                    return (true, "Product has been updated successfully.");
                }
            }

            return (false, "Some error occured.");
        }
        catch (Exception ex)
        {
            return(false, $"There is an error: {ex}");
        }
    }

    public async Task<List<ProductViewModal>?> GetProductList()
    {
        try
        {
            return await _unitOfWork.Products.GetSelectedListAsync(
                p => !p.IsDeleted,
                p => new ProductViewModal{
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductRate = p.ProductRate,
                    StockQuantity = p.StockQuantity,
                    Category = p.Category
                }
            );
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<ProductViewModal?> GetProductById(int productId)
    {
        try
        {
            return await _unitOfWork.Products.GetFirstOrDefaultSelected(
                p => p.Id == productId && !p.IsDeleted,
                p => new ProductViewModal{
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductRate = p.ProductRate,
                    StockQuantity = p.StockQuantity,
                    Category = p.Category,
                    Description = p.Description
                }
            );
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<(bool isSuccess, string message)> DeleteProductAsync(int productId)
    {
        try
        {

            Product? existingProduct = await _unitOfWork.Products.GetFirstOrDefault(p => p.Id == productId && !p.IsDeleted);

            if(existingProduct != null) {
                existingProduct.IsDeleted = true;
                bool isUpdated = await _unitOfWork.Products.UpdateAsync(existingProduct);
                
                if(isUpdated) {
                    return (true, "Product has been deleted successfully.");
                }
            }

            return (false, "Some error occured.");
        }
        catch (Exception ex)
        {
            return(false, $"There is an error: {ex}");
        }
    }
}
