using Canteen.Core.BusinessModels;
using Canteen.Core.DataAccess;
using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Canteen.Core.Managers
{
    public class AuthManager : IAuthManger
    {
        private readonly IActiveRepository _repo;
        private readonly ILogger<IAuthManger> _log;
        private readonly UserManager<AppUser> _userManager;
        private readonly JWT _secure;
        public AuthManager(IActiveRepository repo, ILogger<IAuthManger> log, UserManager<AppUser> manager, IOptions<JWT> secure)
        {
            _repo = repo;
            _log = log;
            _secure = secure.Value;
        }

        public TokenResponse LoginUser(AuthenticateModel model)
        {
            var findUser = _repo.GetById(model.Username);
            throw new System.NotImplementedException();
        }
    }

    public interface IAuthManger
    {
        TokenResponse LoginUser(AuthenticateModel model);
    }
}
