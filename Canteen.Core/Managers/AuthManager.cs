using Canteen.Core.BusinessModels;
using Canteen.Core.DataAccess;
using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

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

        public TokenResponse LoginUser(AuthenticateUserModel model, string password)
        {
            throw new NotImplementedException();
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
    }


    public interface IAuthManager
    {
        TokenResponse LoginUser(AuthenticateUserModel model, string password);
        UserModel CreateUser(UserModel user, string password);
    }
}
