using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class PromotionResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public PromotionResponseModel(Promotion promotion)
        {
            Id = promotion.Id;
            Name = promotion.Name;
            Rate = promotion.Rate;
            StartDate = promotion.StartDate;
            EndDate = promotion.EndDate;
        }
    }
}
