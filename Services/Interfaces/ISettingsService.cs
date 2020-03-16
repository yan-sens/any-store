using DAL.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISettingsService
    {
        Task<List<CurrencyModel>> GetAllCurrencies();
        Task CreateCurrency(CurrencyModel model);
        Task UpdateCurrency(CurrencyModel model);
        Task RemoveCurrency(Guid id);
    }
}
