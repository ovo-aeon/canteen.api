using Canteen.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    // Security stamp is for salt
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CustomerOrders> Orders { get; set; }

    }
}
