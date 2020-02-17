using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
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
    }
}
