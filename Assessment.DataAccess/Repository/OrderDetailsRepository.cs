using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;

namespace Assessment.DataAccess.Repository;

public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository
{
    private readonly MyDbContext _context;

    public OrderDetailsRepository(MyDbContext context) : base(context) {
        _context = context;
    }
}
