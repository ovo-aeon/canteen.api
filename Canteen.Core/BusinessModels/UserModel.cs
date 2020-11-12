using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using Newtonsoft.Json;
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
        [MinLength(11, ErrorMessage = "Phone can't be less than 11 characters")]
        public string Phone { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password can't be less than 8 characters")]
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
            userEntity.IsActive = true;
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
        [MinLength(8)]
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        internal TokenResponse CreateResp(string username, string token, string email, string refreshToken)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email)) return null;

            var resp = new TokenResponse();
            resp.JwtToken = token;
            resp.Email = email;
            resp.Username = username;
            resp.RefreshToken = refreshToken;
            return resp;
        }

    }



}
