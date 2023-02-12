using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepositıry<T> where T : class
    {
        Task<T> GetByIdAsync(int id); //Async olduğu için task dönütoruz.
        IQueryable<T> GetAll(); 

        //productRepository.where(x=>x.id>5).OrderBy.ToListAsync(); 
        //Veritabanından alırken sıralama yapabilmek için IQuerable döneriz.Böyle yapmazsak ilk önce veriyi çeker sonra memory de sıralama yapar.Bu da memory i yorar. 
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //IQueryable döndüğümüzde  yazmış olduğumuz sorgular direkt veritabanına gitmez ToList gibi metotları çağırdığımızda veritabanına gider.
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);  //Var mı yok mu kontrol etmek için AnyAsync metodunu yazdık
        Task AddRangeAsync(IEnumerable<T> entities); //List yerine IEnumerable interface ini kullandık.BP için
        Task AddAsync(T entity);
        void Update(T entity); //Update veya remove metotların EF Core tarafında Async metotları yoktur.
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
