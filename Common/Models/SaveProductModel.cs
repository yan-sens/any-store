using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class SaveProductModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public decimal SellingPrice { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid CategoryId { get; set; }
        public string Image { get; set; }
        public List<string> Images { get; set; }
    }
}
