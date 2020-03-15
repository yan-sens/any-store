using DAL.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetPromotions();
        Task<Promotion> AddPromotion(SavePromotionModel model);
        Task<Promotion> UpdatePromotion(SavePromotionModel model);
        Task UpdatePromotionProduct(UpdatePromotionProductModel model);
        Task<List<PromotionMapping>> GetPromotionProducts(Guid? promotionId);
        Task RemovePromotion(Guid id);
    }
}
