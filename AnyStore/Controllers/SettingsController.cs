using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnyStore.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICategoryService categoryService, IProductService productService, ISettingsService settingsService) : base(userManager, configuration, categoryService, productService)
        {
            _settingsService = settingsService;
        }

        public Task<List<Currency>> GetAllCurrencies()
        {
            return _settingsService.GetAllCurrencies();
        }
    }
}
