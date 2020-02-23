using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Promotion : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<PromotionMapping> ProductMappings { get; set; }
    }
}
