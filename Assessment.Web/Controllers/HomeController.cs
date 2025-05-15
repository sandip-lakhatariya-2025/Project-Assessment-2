using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Assessment.Models.ViewModel;
using Assessment.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Web.Controllers;

[Authorize(Roles = "Admin")]
public class HomeController : Controller
{

    private readonly IProductService _productService;

    public HomeController(IProductService productService) {
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAddOrEditProductModal(int productId) {
        if(productId == 0) {
            return PartialView("_AddOrEditProductModal");
        }
        ProductViewModal? product = await _productService.GetProductById(productId);
        return PartialView("_AddOrEditProductModal", product);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEditProduct(ProductViewModal product) {
        
        (bool isSuccess, string message) result;

        if(product.Id == 0) {
            result = await _productService.AddProductAsync(product);
        }
        else {
            result = await _productService.EditProductAsync(product);
        }
        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }

    [HttpGet]
    public async Task<IActionResult> GetProductList() {
        List<ProductViewModal>? products = await _productService.GetProductList();
        return PartialView("_ProductList", products);
    }

    [HttpGet]
    public IActionResult GetDeleteModal(int id) {
        return PartialView("_DeleteModal", id);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int productId) {
        
        var result = await _productService.DeleteProductAsync(productId);

        return Json(new { isSuccess = result.isSuccess, message = result.message });
    }
}
