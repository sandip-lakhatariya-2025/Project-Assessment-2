using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assessment.Services.IServices;

public interface IDropdownService
{
    List<SelectListItem> GetProductList();
}
