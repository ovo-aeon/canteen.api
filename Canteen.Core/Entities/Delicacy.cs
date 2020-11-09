namespace Canteen.Core.Entities
{
    public class Delicacy:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Pricing { get; set; }
        public bool InStock { get; set; }
        public int TotalSold { get; set; }
        public int TotalInStock { get; set; }
        public int Rating { get; set; }
        public string Img { get; set; }

    }
}