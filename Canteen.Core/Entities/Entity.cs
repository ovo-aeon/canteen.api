using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
