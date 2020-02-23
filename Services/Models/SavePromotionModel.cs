using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class SavePromotionModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
