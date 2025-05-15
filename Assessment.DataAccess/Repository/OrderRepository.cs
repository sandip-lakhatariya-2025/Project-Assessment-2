using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;

namespace Assessment.DataAccess.Repository;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly MyDbContext _context;

    public OrderRepository(MyDbContext context) : base(context) {
        _context = context;
    }
}
