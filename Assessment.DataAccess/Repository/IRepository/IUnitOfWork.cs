namespace Assessment.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IOrderDetailsRepository OrderDetails { get; }
}
