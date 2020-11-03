using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Canteen.Core.BusinessModels
{
    public class AuthenticateModel
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
