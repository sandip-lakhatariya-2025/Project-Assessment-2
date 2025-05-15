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

    public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter) 
    {
        return await _dbSet.Where(filter).FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetFirstOrDefaultSelected<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
    {
        return await _dbSet.Where(filter).Select(selector).FirstOrDefaultAsync();
    }

    public async Task<bool> InsertAsync(T entity) {
        await _context.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity) {
        _context.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<TResult>> GetSelectedListAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
    {
        return await _dbSet.Where(filter).OrderBy(x => EF.Property<int>(x, "Id")).Select(selector).ToListAsync();
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> filter) {
        return await _dbSet.AnyAsync(filter);
    }
    
    public async Task<bool> InsertListAsync(List<T> list) {
        _context.AddRange(list);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateListAsync(List<T> list) {
        _context.UpdateRange(list);
        return await _context.SaveChangesAsync() > 0;
    }

}
