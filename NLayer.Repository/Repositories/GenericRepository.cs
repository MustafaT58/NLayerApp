 using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepositıry<T> where T : class
    {
        protected readonly AppDbContext _context;   //Sadece Constructor içinde set edebilmemiz için readonly tanımladık.
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            //AsNoTracking EF Core çekmiş olduğu dataları memory e almaması izlememesi için kullanılır
            //Herhangi bir güncelleme,silme yada ekleme işlemi yapılmadığı için böyle kullanmak doğrudur ve performans arttırır.
            return _dbSet.AsNoTracking().AsQueryable(); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); 
        }

        //Burada veri işlemleri Db de değil de memory de olduğu için Update ve Remove Metotlarının Async özelliği yoktur.!!
        public void Remove(T entity) 
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
