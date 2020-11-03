using Canteen.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Core.Utilities
{
    public class Seed
    {
        public static async Task SeedEssentialsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles // AuthorizationRoles is a custom class
            await roleManager.CreateAsync(new IdentityRole(AuthoriationRoles.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(AuthoriationRoles.Roles.Vendor.ToString()));
            await roleManager.CreateAsync(new IdentityRole(AuthoriationRoles.Roles.Customer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(AuthoriationRoles.Roles.Watcher.ToString()));


            //Seed Default User
            var defaultUser = new AppUser { UserName = AuthoriationRoles.default_username, Email = AuthoriationRoles.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, AuthoriationRoles.default_password);
                await userManager.AddToRoleAsync(defaultUser, AuthoriationRoles.default_role.ToString());
            }
        }
    }
}
