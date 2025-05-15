using System.Security.Claims;
using System.Threading.Tasks;
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

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Index()
    {
        int customerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        List<OrderDetailsViewModel>? orderDetailsList = await _orderService.GetAllOrderOfCustomer(customerId);
        return View(orderDetailsList);
    }

    [Authorize(Roles = "User")]
    public IActionResult OrderCart()
    {
        return View();
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetProductDetails(int productId) {
        ProductViewModal? product = await _productService.GetProductById(productId);
        return Json(product);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateNewOrder(List<ProductViewModal> OrderDetails) {
        int customerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _orderService.AddNewOrderAsync(OrderDetails, customerId);
        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }


    [Authorize(Roles = "Admin")]
    public IActionResult AllOrderHistory()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersList()
    {
        List<OrderDetailsViewModel>? orderDetailsList = await _orderService.GetAllOrders();
        return PartialView("_AllOrdersList", orderDetailsList);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string orderStatus){
        var result = await _orderService.UpdateOrderStatusAsync(orderId, orderStatus);
        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }
}
