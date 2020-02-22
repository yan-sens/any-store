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
    public class SettingsService : ISettingsService
    {
        private readonly AnyStoreContext _dbContext;

        public SettingsService(AnyStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Currency>> GetAllCurrencies()
        {
            return await _dbContext.Currencies.ToListAsync();
        }

        public async Task CreateCurrency(CurrencyModel model)
        {
            _dbContext.Add(new Currency()
            {
                Name = model.Name,
                Display = model.Display,
                CreateDate = DateTime.Now
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCurrency(CurrencyModel model)
        {
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (currency == null)
                return;

            currency.Name = model.Name;
            currency.Display = model.Display;
            currency.UpdateDate = DateTime.Now;

            _dbContext.Update(currency);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveCurrency(Guid id)
        {
            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == id);
            if (currency == null)
                return;

            _dbContext.Remove(currency);
            await _dbContext.SaveChangesAsync();
        }
    }
}
