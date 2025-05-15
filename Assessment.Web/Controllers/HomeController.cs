using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Assessment.Models.ViewModel;
using Assessment.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Web.Controllers;

public class HomeController : Controller
{

    private readonly IProductService _productService;

    public HomeController(IProductService productService) {
        _productService = productService;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetAddOrEditProductModal(int productId) {
        if(productId == 0) {
            return PartialView("_AddOrEditProductModal");
        }
        return PartialView("_AddOrEditProductModal");
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEditProduct(ProductViewModal product) {
        var result = await _productService.AddProductAsync(product);
        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        return View();
    }
}
