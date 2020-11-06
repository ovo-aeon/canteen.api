using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : CanteenContext
    {
        TContext Context { get; }
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext:CanteenContext,IDisposable
    {
        public TContext Context { get; }
        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)GetRepositoryInstance(typeof(TEntity), new Repository<TEntity>(Context));
        }
        internal object GetRepositoryInstance(Type type, object repo)
        {
            return repo;
        }
        public int Commit()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {

            Context?.Dispose();
        }
    }
}
