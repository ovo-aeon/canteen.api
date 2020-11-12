using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    public class CustomerOrders:Entity
    {
        public virtual AppUser Customer { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public decimal Amount { get; set; }

    }
}
