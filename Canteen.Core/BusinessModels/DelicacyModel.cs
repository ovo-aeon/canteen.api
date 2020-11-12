using System;
using System.Collections.Generic;
using System.Text;

namespace Canteen.Core.BusinessModels
{
    public class DelicacyModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Pricing { get; set; }
        public int TotalInStock { get; set; }
        public string Img { get; set; }
    }
}
