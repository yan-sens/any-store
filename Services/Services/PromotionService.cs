using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<PromotionResponseModel>> GetPromotions()
        {
            var result = await _dbContext.Promotions.Select(x => new PromotionResponseModel(x)).ToListAsync();
            return result;
        }

        public async Task<Promotion> AddPromotion(SavePromotionModel model)
        {
            var promotion = new Promotion()
            {
                Name = model.Name,
                Rate = model.Rate,
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
                CreateDate = DateTime.Now
            };

            _dbContext.Add(promotion);
            await _dbContext.SaveChangesAsync();

            return promotion;
        }

        public async Task<Promotion> UpdatePromotion(SavePromotionModel model)
        {
            var promotion = await _dbContext.Promotions.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (promotion == null)
                return null;

            promotion.Name = model.Name;
            promotion.Rate = model.Rate;
            promotion.StartDate = DateTime.Parse(model.StartDate);
            promotion.EndDate = DateTime.Parse(model.EndDate);
            promotion.UpdateDate = DateTime.Now;

            _dbContext.Update(promotion);
            await _dbContext.SaveChangesAsync();

            return promotion;
        }

        public async Task<List<PromotionMappingResponseModel>> GetPromotionProducts(Guid? promotionId)
        {
            return await _dbContext.PromotionMappings.Where(x => promotionId == null || x.PromotionId == promotionId).Select(x => new PromotionMappingResponseModel(x)).ToListAsync();
        }

        public async Task UpdatePromotionProduct(UpdatePromotionProductModel model)
        {
            var mappedProduct = await _dbContext.PromotionMappings
                                            .FirstOrDefaultAsync(x => x.ProductId == model.ProductId && x.PromotionId == model.PromotionId);

            if(mappedProduct == null && model.Active)
            {
                var newMapping = new PromotionMapping()
                {
                    ProductId = model.ProductId,
                    PromotionId = model.PromotionId,
                    CreateDate = DateTime.Now
                };

                _dbContext.Add(newMapping);
                await _dbContext.SaveChangesAsync();
            }
            else if (mappedProduct != null && !model.Active)
            {
                _dbContext.Remove(mappedProduct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemovePromotion(Guid id)
        {
            var promotions = await _dbContext.Promotions.Where(x => x.Id == id).ToListAsync();
            var promotionMappings = await _dbContext.PromotionMappings.Where(x => x.PromotionId == id).ToListAsync();

            promotionMappings.ForEach(x => _dbContext.Remove(x));
            promotions.ForEach(x => _dbContext.Remove(x));

            await _dbContext.SaveChangesAsync();
        }
    }
}
