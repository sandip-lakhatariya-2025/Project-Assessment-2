using Assessment.Models.ViewModel;

namespace Assessment.Services.IServices;

public interface IOrderService
{
    Task<(bool isSuccess, string message)> AddNewOrderAsync(List<ProductViewModal> OrderDetails, int CustomerId);
}
