using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.Entities
{
    public class Restaurant:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Address { get; set; }
        public string SearchKeyWords { get; set; }
        public string Phone { get; set; }
        public string LogoName { get; set; }
        public virtual ICollection<Delicacy> Delicacies { get; set; }
        public decimal Revenue { get; set; }
        public virtual ICollection<Review> Reviews { get;set; }
        public int Rating { get; set; }
    }

    public class Review:Entity
    {
        public virtual AppUser Customer { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public string Comment { get; set; }
    }
}
