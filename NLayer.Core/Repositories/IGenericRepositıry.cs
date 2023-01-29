using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepositıry<T> where T : class
    {
        Task<T> GetByAsync(int id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        //productRepository.where(x=>x.id>5).OrderBy.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); 
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
