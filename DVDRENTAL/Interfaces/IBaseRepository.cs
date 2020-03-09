using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRENTAL.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {

        IQueryable<T> GetAll();
        int Save();

        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        T GetFirstWhere(System.Linq.Expressions.Expression<Func<T, bool>> func);
        IQueryable<T> GetAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> func);
        
    }
}
