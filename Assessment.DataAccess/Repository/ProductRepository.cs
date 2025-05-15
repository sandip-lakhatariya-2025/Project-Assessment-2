using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;

namespace Assessment.DataAccess.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly MyDbContext _context;

    public ProductRepository(MyDbContext context) : base(context) {
        _context = context;
    }
}
