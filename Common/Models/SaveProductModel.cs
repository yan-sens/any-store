using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class SaveProductModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public decimal SellingPrice { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
