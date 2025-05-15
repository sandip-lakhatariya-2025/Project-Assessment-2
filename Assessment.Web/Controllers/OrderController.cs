using System.Security.Claims;
using Assessment.Models.ViewModel;
using Assessment.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Web.Controllers;

public class OrderController : Controller
{

    private readonly IProductService _productService;
    private readonly IOrderService _orderService;

    public OrderController(IProductService productService, IOrderService orderService) {
        _productService = productService;
        _orderService = orderService;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetProductDetails(int productId) {
        ProductViewModal? product = await _productService.GetProductById(productId);
        return Json(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewOrder(List<ProductViewModal> OrderDetails) {
                // var result = await _productService.DeleteProductAsync(productId);

        Console.WriteLine("--------------> "+OrderDetails.First().ProductName);
        Console.WriteLine("--------------> "+OrderDetails.First().ProductRate);
        Console.WriteLine("--------------> "+OrderDetails.First().ProductQuantity);
        Console.WriteLine("--------------> "+OrderDetails.First().Id);
        int customerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _orderService.AddNewOrderAsync(OrderDetails, customerId);
        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }
}
