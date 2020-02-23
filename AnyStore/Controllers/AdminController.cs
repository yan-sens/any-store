using AnyStore.Models;
using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyStore.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IPromotionService _promotionService;
        public AdminController(UserManager<ApplicationUser> userManager,
                                IConfiguration configuration, 
                                ICategoryService categoryService, 
                                IProductService productService,
                                IPromotionService promotionService) 
            : base(userManager, configuration, categoryService, productService)
        {
            _promotionService = promotionService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Categories()
        {
            return View();
        }

        [Authorize]
        public IActionResult Products()
        {
            return View();
        }

        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }

        [Authorize]
        public IActionResult Promotions()
        {
            return View();
        }

        [Authorize]
        public IActionResult Warehouse()
        {
            return View();
        }

        [Authorize]
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            categories.ForEach(x => {
                x.Categories = null;
                x.ParentCategoryName = x.ParentCategory?.Name;
            });
            return categories;
        }

        [Authorize]
        public async Task<List<Category>> GetCategoriesWithChild()
        {
            var categories = await _categoryService.GetCategoriesWithChild();
            categories.ForEach(x => {
                x.Categories = null;
                x.ParentCategory = null;
                x.ParentCategoryName = x.ParentCategory?.Name;
            });
            return categories;
        }

        [Authorize]
        public async Task<List<Category>> GetCategoriesForProduct()
        {
            var categories = await _categoryService.GetCategoriesForProduct();
            categories.ForEach(x => {
                x.Categories = null;
                x.ParentCategory = null;
            });
            return categories;
        }

        [Authorize]
        public async Task RemoveCategory(string id)
        {
            await _categoryService.RemoveCategory(Guid.Parse(id));
        }

        [Authorize]
        public async Task AddCategory(CategoryRequestModel model)
        {
            Guid? parentCategoryId = null;
            if (!String.IsNullOrEmpty(model.ParentCategoryId))
                parentCategoryId = Guid.Parse(model.ParentCategoryId);

            var category = new Category()
            {
                Name = model.Name,
                CreateDate = DateTime.Now,
                Description = model.Description,
                HasChildren = model.HasChildren,
                ParentCategoryId = parentCategoryId,
                Title = model.Title
            };

            await _categoryService.AddCategory(category);
        }

        [Authorize]
        public async Task UpdateCategory(CategoryRequestModel model)
        {
            Guid? parentCategoryId = null;
            if (!String.IsNullOrEmpty(model.ParentCategoryId))
                parentCategoryId = Guid.Parse(model.ParentCategoryId);

            var category = new Category()
            {
                Id = Guid.Parse(model.Id),
                Name = model.Name,
                CreateDate = DateTime.Now,
                Description = model.Description,
                HasChildren = model.HasChildren,
                ParentCategoryId = parentCategoryId,
                Title = model.Title
            };

            await _categoryService.UpdateCategory(category);
        }        
    }
}
