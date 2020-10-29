using Canteen.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Models
{
    public class AppUser: Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public TypeOfUser UserType { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CustomerOrders> Orders { get; set; }

    }
}
