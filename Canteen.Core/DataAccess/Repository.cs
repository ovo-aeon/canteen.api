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

/*  
 *   long Save(Entity entity);
        long SaveAsync(Entity entity);
        void SaveAll(IEnumerable<object> entities);
        void Delete(object entity);
        IQueryable<TEntity> Get<TEntity>();
        TEntity Get<TEntity>(long id);
 *  public class Repository : IRepository, IDisposable
    {
        private ILogger<Repository> _log;
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public Repository(ILogger<Repository> log, ISession session)
        {
            _log = log;
            _session = session;
        }

        #region Transaction and session management members

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            // _transaction will be replace with new transaction
            _transaction.Commit();
            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            // _session must be closed and disposed
            if (_transaction != null)
            {
                _transaction.Rollback();
                CloseTransaction();
                CloseSession();
            }
        }

        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        private void CloseSession()
        {
            if (_session != null && _session.IsConnected)
            {
                _session.Close();
                _session.Dispose();
                _session = null;
            }
        }

        #endregion

        #region IDisposable members

        public void Dispose()
        {
            if (_transaction != null)
            {
                // Commit transactions by default except rollback is explicity called
                CommitTransaction();
            }

            if (_session != null)
            {
                try
                {
                    // Persist session transactions
                    _session.Flush();
                    CloseSession();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    _log.LogCritical($"{ex.Message}");
                }

            }
        }

        #endregion

        #region IRepository generic members

        public virtual long Save(Entity entity)
        {
            try
            {
                _session.SaveOrUpdate(entity);
                return entity.Id;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                _log.LogCritical($"{ex.Message}");
                return 0;
            }

        }

        public virtual long SaveAsync(Entity entity)
        {
            try
            {
                _session.SaveOrUpdateAsync(entity);
                return entity.Id;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                _log.LogCritical($"{ex.Message}");
                return 0;
            }

        }

        public virtual void SaveAll(IEnumerable<object> entities)
        {
            try
            {
                foreach (var entity in entities) _session.SaveOrUpdate(entity);
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                _log.LogCritical($"{ex.Message}");
            }
        }

        public virtual void Delete(object entity)
        {
            _session.Delete(entity);
        }

        public virtual TEntity Get<TEntity>(long id)
        {
            return _session.Load<TEntity>(id);
        }

        public virtual IQueryable<TEntity> Get<TEntity>()
        {
            try
            {
                return (from entity in _session.Query<TEntity>() select entity);
            }
            catch (Exception ex)
            {
                _log.LogCritical($"_session.Query<TEntity>() - {ex.Message}");
                return new List<TEntity>().AsQueryable();
            }
        }

        #endregion

    }

    public interface IRepository : IDisposable
    {
        void BeginTransaction();
        void RollbackTransaction();
        long Save(Entity entity);
        long SaveAsync(Entity entity);
        void SaveAll(IEnumerable<object> entities);
        void Delete(object entity);
        IQueryable<TEntity> Get<TEntity>();
        TEntity Get<TEntity>(long id);
    }
*/
