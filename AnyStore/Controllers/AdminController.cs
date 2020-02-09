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
        private readonly ICategoryService _categoryService;
        public AdminController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICategoryService categoryService) : base(userManager, configuration)
        {
            _categoryService = categoryService;
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
        public async Task RemoveCategory(string id)
        {
            await _categoryService.RemoveCategory(Guid.Parse(id));
        }
    }
}
