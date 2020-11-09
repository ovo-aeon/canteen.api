using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Utilities
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}
