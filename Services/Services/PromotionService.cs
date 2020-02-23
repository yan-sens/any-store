using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly AnyStoreContext _dbContext;
        public PromotionService(AnyStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            return await _dbContext.Promotions.ToListAsync();
        }

        public async Task<Promotion> AddPromotion(SavePromotionModel model)
        {
            var promotion = new Promotion()
            {
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreateDate = DateTime.Now
            };

            _dbContext.Add(promotion);
            await _dbContext.SaveChangesAsync();

            return promotion;
        }
    }
}
