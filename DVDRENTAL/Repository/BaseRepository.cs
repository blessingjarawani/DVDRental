using DVDRENTAL.Entities;
using DVDRENTAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRENTAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected rentalEntities _rentalEntities;
        protected DbSet<T> DbSet { get; set; }
        public BaseRepository(rentalEntities rentalEntities)
        {
            _rentalEntities = rentalEntities ?? throw new ArgumentException(nameof(rentalEntities));
            DbSet = rentalEntities.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
        public int Save()
        {
            return _rentalEntities.SaveChanges();
        }
        public void Update(T entity)
        {
            _rentalEntities.Entry(entity).State = EntityState.Modified;
        }
        public T GetFirstWhere(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            return DbSet.Where(func).FirstOrDefault();
        }

       
        public IQueryable<T> GetAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            return DbSet.Where(func);
        }


    }
}
