using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CanteenContext _context;
        public UnitOfWork(CanteenContext context)
        {
            _context = context;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        int Complete();

    }
}
