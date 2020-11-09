using Canteen.Core.BusinessModels;
using Canteen.Core.DataAccess;
using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Canteen.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly ILogger<IAuthManager> _log;
        private readonly IUnitOfWork _uow;
        private readonly Jwt _secure;
        public AuthManager(ILogger<IAuthManager> log, IOptions<Jwt> secure, IUnitOfWork uow)
        {
            _log = log;
            _uow = uow;
            _secure = secure.Value;
        }

        public (TokenResponse Response, string Message) LoginUser(AuthenticateUserModel model, string password)
        {
            if (model == null || password == null) return (null,"Password or username incorrect!");
            var user = _uow.GetRepository<AppUser>().FindByCondition(u => u.Username == model.Username && u.IsActive == true);
            var verifyPwd = AuthHelperClass.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (user == null && verifyPwd == false) return (null, "Something is wrong check your entries!");
            var role = Enum.GetName(typeof(Roles), user.Role);
            var claims = new ClaimsIdentity(new[] { new Claim("id", $"{user.Id}"), new Claim(ClaimTypes.Name, model.Username), new Claim(ClaimTypes.Role, role) });

            var token = AuthHelperClass.GenerateJwtToken(_secure.Key, (int)_secure.DurationInMinutes, claims);
            var refreshToken = AuthHelperClass.GenerateRefreshToken();
            claims.AddClaim(new Claim("token", token));
            var resp = new TokenResponse().CreateResp(model.Username, token, user.Email, refreshToken);
            // Save tokens to DB

            _uow.GetRepository<AppUser>().Update(user);
            _uow.Commit();
            return (resp, "User login Authenticated");
        }

        public UserModel CreateUser(UserModel user, string password)
        {
            // validation

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            var userExists = _uow.GetRepository<AppUser>().FindByCondition(u => u.Username.Equals(user.Username) && user.Email.Equals(user.Email));

            if (userExists != null)
                throw new Exception("Username \"" + user.Username + "\" is already taken");

            var userEntity = new UserModel().Create(user, password);
            _uow.GetRepository<AppUser>().Insert(userEntity);
            var savedEntityId = _uow.Commit();
            _log.LogInformation($"Saved => {user.Username} {savedEntityId}", user.Username, savedEntityId);
            user.Password = "";
            return user;
        }

        public AppUser GetUser(int id)
        {
           if(id != 0)
            {
                var user = _uow.GetRepository<AppUser>().GetOne(id);
                return user;
            }
            return null;
        }
    }


    public interface IAuthManager
    {
        (TokenResponse Response, string Message) LoginUser(AuthenticateUserModel model, string password);
        UserModel CreateUser(UserModel user, string password);
        AppUser GetUser(int id);


    }
}
