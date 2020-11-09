using Canteen.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    // Security stamp is for salt
    public class AppUser: Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Roles Role { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CustomerOrders> Orders { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
