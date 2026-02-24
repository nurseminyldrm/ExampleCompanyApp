using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Core.Repositories
{
    public interface IGenericRepository<T>where T: class
    {
        Task<T>GetByIdAsync(int id);
        IQueryable<T>GetAll(Expression<Func<T,bool>>?expression =null);
        IQueryable<T>Where(Expression<Func<T,bool>> expression);
        Task<bool>AnyAsync(Expression<Func<T,bool>> expression);
        Task AddAsync (T entity);
        Task AddRange (IEnumerable<T> entities); 
        void Update (T entity);
        void Remove (T entity);
        void RemoveRange (IEnumerable<T> entities);
    }
}
