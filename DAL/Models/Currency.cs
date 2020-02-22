
using System.Collections.Generic;

namespace DAL.Models
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public List<Product> Products { get; set; }
    }
}
