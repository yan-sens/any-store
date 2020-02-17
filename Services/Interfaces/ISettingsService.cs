using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISettingsService
    {
        Task<List<Currency>> GetAllCurrencies();
    }
}
