using Canteen.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.DataAccess
{
    public class CanteenContext : DbContext
    {
        public CanteenContext(DbContextOptions<CanteenContext> options) : base(options)
        {
            
        }

        // set DB tables
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<CustomerOrders> CustomerOrders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Delicacy> Delicacies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
