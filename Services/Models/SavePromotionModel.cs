using System;

namespace Services.Models
{
    public class SavePromotionModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
