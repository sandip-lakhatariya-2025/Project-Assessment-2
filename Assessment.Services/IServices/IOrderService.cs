using Assessment.Models.ViewModel;

namespace Assessment.Services.IServices;

public interface IOrderService
{
    Task<(bool isSuccess, string message)> AddNewOrderAsync(List<ProductViewModal> OrderDetails, int CustomerId);

    Task<List<OrderDetailsViewModel>?> GetAllOrderOfCustomer(int customerId);
    Task<List<OrderDetailsViewModel>?> GetAllOrders();
    Task<(bool isSuccess, string message)> UpdateOrderStatusAsync(int orderId, string orderStatus);
}
