using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Models
{
    public class CustomerOrders:Entity
    {
        public virtual AppUser AppUser { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public bool OrderStatus { get; set; }

    }
}
