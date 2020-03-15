using AnyStore.Models;
using Common.Models;
using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyStore.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICategoryService categoryService, IProductService productService) : base(userManager, configuration, categoryService, productService)
        { }

        [AllowAnonymous]
        public async Task<IActionResult> Products(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);          
            var model = new ProductsViewModel(CategoryMenuItems, category);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Product(Guid id)
        {
            var product = await _productService.GetProductById(id);
            var model = new ProductViewModel(CategoryMenuItems, product);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId)
        {
            var result = await _productService.GetProductsByCategoryId(categoryId);
            return Json(result);
        }

        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetAllProducts();

            return Json(result);
        }

        [Authorize]
        public async Task<List<ProductImage>> GetProductImagesByProductId(Guid productId)
        {
            return await _productService.GetProductImagesByProductId(productId);
        }

        [Authorize]
        public async Task CreateProduct(SaveProductModel model)
        {
            await _productService.CreateProduct(model);
        }

        [Authorize]
        public async Task UpdateProduct(SaveProductModel model)
        {
            await _productService.UpdateProduct(model);
        }

        [Authorize]
        public async Task<IActionResult> GetPromotionProducts()
        {
            var result = await _productService.GetPromotionProducts();
            return Json(result);
        }        

        [Authorize]
        public async Task RemoveProduct(string id)
        {
            await _productService.RemoveProduct(Guid.Parse(id));
        }
    }
}
