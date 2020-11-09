using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Utilities
{
    public class Security
    {

    }
    public class Jwt
    {
        public string Key { get; set; } = "C1CF4B7DC4C4T995UROO449DFKIIONG";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
