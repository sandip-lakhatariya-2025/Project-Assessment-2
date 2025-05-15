using System.Linq.Expressions;
using Assessment.DataAccess.Data;
using Assessment.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DataAccess.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MyDbContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(MyDbContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<TResult?> GetFirstOrDefaultSelected<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
    {
        return await _dbSet.Where(filter).Select(selector).FirstOrDefaultAsync();
    }

    public async Task<bool> InsertAsync(T entity) {
        await _context.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

}
