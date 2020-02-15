using Common.Classes;
using DAL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.Collections.Generic;

namespace AnyStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> UserManager;
        protected ApplicationUser CurrentUser;
        protected IConfiguration Configuration;
        protected ICategoryService _categoryService;
        protected List<CategoryMenuItem> CategoryMenuItems = new List<CategoryMenuItem>();

        protected BaseController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICategoryService categoryService)
        {
            UserManager = userManager;
            Configuration = configuration;
            _categoryService = categoryService;

            CategoryMenuItems = _categoryService.GetCategoryMenuItems().Result;
        }
    }
}