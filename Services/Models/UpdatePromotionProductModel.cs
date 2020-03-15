using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class UpdatePromotionProductModel
    {
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public bool Active { get; set; }
    }
}
