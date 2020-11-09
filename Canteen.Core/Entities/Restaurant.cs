using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    public class Restaurant:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string SearchKeyWords { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public ICollection<Delicacy> Delicacies { get; set; }
    }
}
