using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Manufacture { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice {get; set; }
        public decimal SellingPrice { get; set; }
        public Guid CategoryId { get; set; }
    }
}
