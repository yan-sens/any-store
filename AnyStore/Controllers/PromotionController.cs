﻿using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyStore.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(UserManager<ApplicationUser> userManager,
                                IConfiguration configuration,
                                ICategoryService categoryService,
                                IProductService productService,
                                IPromotionService promotionService)
            : base(userManager, configuration, categoryService, productService)
        {
            _promotionService = promotionService;
        }

        [Authorize]
        public async Task<List<Promotion>> GetPromotions()
        {
            return await _promotionService.GetPromotions();
        }

        [Authorize]
        public async Task<Promotion> AddPromotion(SavePromotionModel model)
        {
            return await _promotionService.AddPromotion(model);
        }

        [Authorize]
        public async Task<Promotion> UpdatePromotion(SavePromotionModel model)
        {
            return await _promotionService.UpdatePromotion(model);
        }

        [Authorize]
        public async Task<IActionResult> UpdatePromotionProduct(UpdatePromotionProductModel model)
        {
            await _promotionService.UpdatePromotionProduct(model);

            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> GetPromotionProducts(Guid? promotionId)
        {
            var result = await _promotionService.GetPromotionProducts(promotionId);
            return Ok(result);
        }

        [Authorize]
        public async Task RemovePromotion(string id)
        {
            await _promotionService.RemovePromotion(Guid.Parse(id));
        }
    }
}
