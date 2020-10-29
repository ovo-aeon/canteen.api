using Canteen.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class CanteenContext : DbContext
    {
        public CanteenContext(DbContextOptions options) : base(options)
        {
            
        }

        // set DB tables
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CustomerOrders> CstOrders { get; set; }
    }
}
