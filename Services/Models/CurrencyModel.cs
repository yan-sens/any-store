using DAL.Models;
using System;

namespace Services.Models
{
    public class CurrencyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
        public CurrencyModel() { }
        public CurrencyModel(Currency currency)
        {
            Id = currency.Id;
            Name = currency.Name;
            Display = currency.Display;
        }
    }
}
