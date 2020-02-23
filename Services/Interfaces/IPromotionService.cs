using DAL.Models;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetPromotions();
        Task<Promotion> AddPromotion(SavePromotionModel model);
    }
}
