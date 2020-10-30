using Canteen.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class ActiveRepository  : IActiveRepository
    {
        private ILogger<Repository> _log;
        protected readonly CanteenContext ctx;

        public ActiveRepository(ILogger<Repository> log, CanteenContext ctx)
        {
            _log = log;
            this.ctx = ctx;
        }

        #region IRepository generic members

        public long Save(Entity entity)
        {
            ctx.Set<object>().Add(entity);
            ctx.SaveChanges();
            return entity.Id;
        }

        public virtual void SaveAll(IEnumerable<object> entities)
        {
            ctx.Set<object>().AddRange(entities);
        }

        public virtual void Delete(object entity)
        {
            ctx.Set<object>().Remove(entity);
        }

        public virtual object Get(long id)
        {
            return ctx.Set<object>().Find(id);

        }

        public virtual IEnumerable<object> Get()
        {
            return ctx.Set<object>().ToList();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        #endregion

    }

    public interface IActiveRepository : IDisposable
    {
        long Save(Entity entity);
        void SaveAll(IEnumerable<object> entities);
        void Delete(object entity);
        IEnumerable<object> Get();
        object Get(long id);
    }
}
