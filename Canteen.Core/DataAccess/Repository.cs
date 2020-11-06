using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CanteenContext context;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Repository(CanteenContext _context)
        {
            context = _context;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.Add(entity);
        }
        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            context.AddRange(entities);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking();
        }
        public void Delete(T id)
        {
            context.Set<T>().Remove(id);
        }

        public IQueryable<T> Query(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        public T GetOne(object id)
        {
            return context.Set<T>().Find(id);
        }

        public T FindByCondition(Expression<Func<T, bool>> expression)
        {
           return context.Set<T>().Where(expression).FirstOrDefault();
        }

        public List<T> FindManyByCondition(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).ToList();
        }
        public void DeletePlenty(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
    }

    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);
        void Insert(T entity);
        void Insert(IEnumerable<T> entity);
        IEnumerable<T> GetAll();
        T GetOne(object entity);
        T FindByCondition(Expression<Func<T, bool>> expression);
        List<T> FindManyByCondition(Expression<Func<T, bool>> expression);
        void Delete(T entity);
        void DeletePlenty(IEnumerable<T> entities);
        void Update(T entity);
    }
}
