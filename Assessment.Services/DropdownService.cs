using Assessment.DataAccess.Data;
using Assessment.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assessment.Services;

public class DropdownService : IDropdownService
{
    private readonly MyDbContext _context;

    public DropdownService(MyDbContext context) {
        _context = context;
    }

    public List<SelectListItem> GetProductList() {
        return _context.Products.Where(p => !p.IsDeleted).Select(p => new SelectListItem{
            Text = p.ProductName,
            Value = p.Id.ToString()
        }).ToList();
    }
}
