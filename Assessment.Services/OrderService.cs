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

}
