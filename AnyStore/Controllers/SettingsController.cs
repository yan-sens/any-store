using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models;
using System;
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

        public async Task<List<Currency>> GetAllCurrencies()
        {
            return await _settingsService.GetAllCurrencies();
        }

        public async Task CreateCurrency(CurrencyModel model)
        {
            await _settingsService.CreateCurrency(model);
        }

        public async Task UpdateCurrency(CurrencyModel model)
        {
            await _settingsService.UpdateCurrency(model);
        }

        public async Task RemoveCurrency(Guid id)
        {
            await _settingsService.RemoveCurrency(id);
        }
    }
}
