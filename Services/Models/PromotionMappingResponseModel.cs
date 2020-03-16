using DAL.Models;
using System;

namespace Services.Models
{
    public class PromotionMappingResponseModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public ProductResponseModel Product { get; set; }
        public PromotionResponseModel Promotion { get; set; }
        public PromotionMappingResponseModel(PromotionMapping mapping)
        {
            Id = mapping.Id;
            ProductId = mapping.ProductId;
            PromotionId = mapping.PromotionId;

            if(mapping.Product != null)
                Product = new ProductResponseModel(mapping.Product);

            if (mapping.Promotion != null)
                Promotion = new PromotionResponseModel(mapping.Promotion);
        }
    }
}
