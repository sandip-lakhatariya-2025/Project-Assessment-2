using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository;
using Assessment.DataAccess.Repository.IRepository;
using Assessment.Models.Models;


namespace Assessment.DataAccess.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context) : base(context) {
        _context = context;
    }
}
