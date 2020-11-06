using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Canteen.Core.BusinessModels
{
    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }
       
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }
       
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(11, ErrorMessage = "Phone can't be longer than 11 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(60, ErrorMessage = "Password can't be less than 9 characters")]
        public string Password { get; set; }

        internal AppUser Create(UserModel user,string password)
        {
            if (password == null || user==null) return null;
            var userEntity = new AppUser();
            byte[] passwordHash, passwordSalt;
            AuthHelperClass.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.PhoneNumber = user.Phone;
            userEntity.FirstName = user.FirstName;
            userEntity.Username = user.Username;
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            userEntity.CreatedAt = DateTime.Now;
            // Assign every user default role
            userEntity.Role = Roles.Customer;
            return userEntity;
        }
    }
    public class AuthenticateUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        public string JwtToken { get; set; }

        public string Refresh { get; set; }

    }



}
