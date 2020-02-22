using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public string Image { get; set; }
        public int StartRate { get; set; }
        public decimal SellingPrice { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid CategoryId { get; set; }
        [NotMapped]
        public bool Availability { get; set; }
        public Currency Currency { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
