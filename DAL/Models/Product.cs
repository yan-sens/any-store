using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Manufacture { get; set; }
        public string Description { get; set; }
        public int StartRate { get; set; }
        public decimal SellingPrice { get; set; }
        public Guid CategoryId { get; set; }
        [NotMapped]
        public bool Availability { get; set; }
    }
}
