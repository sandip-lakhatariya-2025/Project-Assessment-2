using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository.IRepository;

namespace Assessment.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;
    public UnitOfWork(MyDbContext context) {
        _context = context;
        Users = new UserRepository(_context);
        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
        OrderDetails = new OrderDetailsRepository(_context);
    }
    public IUserRepository Users { get; private set; }
    public IProductRepository Products { get; private set; }
    public IOrderRepository Orders { get; private set; }
    public IOrderDetailsRepository OrderDetails { get; private set; }
}
