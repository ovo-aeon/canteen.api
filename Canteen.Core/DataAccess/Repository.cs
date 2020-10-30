using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class Repository: IRepository
    {
        protected readonly CanteenContext _context;
        public Repository(CanteenContext context)
        {
            _context = context;
        }
        public void Add(object entity)
        {
            _context.Set<object>().Add(entity);
            _context.SaveChanges();
        }
        public void AddRange(IEnumerable<object> entities)
        {
            _context.Set<object>().AddRange(entities);
        }
        public IEnumerable<object> Find(Expression<Func<object, bool>> expression)
        {
            return _context.Set<object>().Where(expression);
        }
        public IEnumerable<object> GetAll()
        {
            return _context.Set<object>().ToList();
        }
        public object GetById(long id)
        {
            return _context.Set<object>().Find(id);
        }
        public void Remove(object entity)
        {
            _context.Set<object>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<object> entities)
        {
            _context.Set<object>().RemoveRange(entities);
        }
    }
    public interface IRepository
    {
        object GetById(long id);
        IEnumerable<object> GetAll();
        IEnumerable<object> Find(Expression<Func<object, bool>> expression);
        void Add(object entity);
        void AddRange(IEnumerable<object> entities);
        void Remove(object entity);
        void RemoveRange(IEnumerable<object> entities);
    }
}
