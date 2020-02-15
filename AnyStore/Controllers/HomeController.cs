using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnyStore.Models;
using Microsoft.AspNetCore.Identity;
using DAL.Models.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace AnyStore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICategoryService categoryService) : base(userManager, configuration, categoryService)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new BaseViewModel();
            model.CategoryMenuItems = CategoryMenuItems;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
