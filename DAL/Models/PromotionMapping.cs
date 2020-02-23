using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class PromotionMapping : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public Product Product { get; set; }
        public Promotion Promotion { get; set; }
    }
}
