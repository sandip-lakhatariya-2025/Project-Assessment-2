using System.Security.Claims;
using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;
using Assessment.Models.ViewModel;
using Assessment.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace Assessment.Services;

public class OrderService : IOrderService
{

    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool isSuccess, string message)> AddNewOrderAsync(List<ProductViewModal> OrderDetails, int CustomerId)
    {
        try
        {

            decimal totalAmount = OrderDetails.Sum(o => o.ProductQuantity * o.ProductRate);

            Order order = new Order{
                CustomerId = CustomerId,
                TotalAmount = totalAmount,
                OrderStatus = "Pending"
            };

            bool isAdded = await _unitOfWork.Orders.InsertAsync(order);

            if(isAdded) {
                List<OrderDetail> orderedProducts = new List<OrderDetail>();
                List<Product> productList = new List<Product>();

                foreach(ProductViewModal product in OrderDetails) {
                    orderedProducts.Add(new OrderDetail{
                        OrderId = order.Id,
                        ProductId = product.Id,
                        ProductRate = product.ProductRate,
                        ProductQuantity = product.ProductQuantity
                    });

                    Product? existingProduct = await _unitOfWork.Products.GetFirstOrDefault(p => p.Id == product.Id);

                    if(existingProduct != null) {
                        existingProduct.StockQuantity = existingProduct.StockQuantity - product.ProductQuantity;
                        productList.Add(existingProduct);
                    }

                }

                await _unitOfWork.OrderDetails.InsertListAsync(orderedProducts);
                await _unitOfWork.Products.UpdateListAsync(productList);

                return (true, "Order has been created successfully.");
            }
            
            return (false, "Some error occured.");
        }
        catch (Exception ex)
        {
            return(false, $"There is an error: {ex}");
        }
    }

    public async Task<List<OrderDetailsViewModel>?> GetAllOrderOfCustomer(int customerId) {
        try
        {
            return await _unitOfWork.Orders.GetSelectedListAsync(
                o => o.CustomerId == customerId,
                o => new OrderDetailsViewModel {
                    Id = o.Id,
                    OrderStatus = o.OrderStatus,
                    TotalAmount = o.TotalAmount,
                    OrderedProducts = o.OrderDetails.Select(od => new ProductViewModal{
                        ProductName = od.Product.ProductName,
                        ProductQuantity = od.ProductQuantity,
                        ProductRate = od.ProductRate,
                    }).ToList()
                }
            );
        }
        catch (Exception)
        {            
            return null;
        }
    }

    public async Task<List<OrderDetailsViewModel>?> GetAllOrders() {
        try
        {
            return await _unitOfWork.Orders.GetSelectedListAsync(
                o => 1 == 1,
                o => new OrderDetailsViewModel {
                    Id = o.Id,
                    OrderStatus = o.OrderStatus,
                    TotalAmount = o.TotalAmount,
                    OrderedProducts = o.OrderDetails.Select(od => new ProductViewModal{
                        ProductName = od.Product.ProductName,
                        ProductQuantity = od.ProductQuantity,
                        ProductRate = od.ProductRate,
                    }).ToList()
                }
            );
        }
        catch (Exception)
        {            
            return null;
        }
    }

    public async Task<(bool isSuccess, string message)> UpdateOrderStatusAsync(int orderId, string orderStatus) {
        try
        {

            Order? existingOrder = await _unitOfWork.Orders.GetFirstOrDefault(o => o.Id == orderId);

            if(existingOrder != null) {
                existingOrder.OrderStatus = orderStatus;
                bool isUpdated = await _unitOfWork.Orders.UpdateAsync(existingOrder);
                
                if(isUpdated) {
                    return (true, "Order Status has been updated successfully.");
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
