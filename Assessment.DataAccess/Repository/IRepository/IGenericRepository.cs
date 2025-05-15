using System.Linq.Expressions;

namespace Assessment.DataAccess.Repository.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<TResult?> GetFirstOrDefaultSelected<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
    Task<bool> InsertAsync(T entity);
    Task<List<TResult>> GetSelectedListAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
}
