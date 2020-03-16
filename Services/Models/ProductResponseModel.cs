using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public string Image { get; set; }
        public int StartRate { get; set; }
        public decimal SellingPrice { get; set; }
        public bool Available { get; set; }
        public string CurrencyName { get; set; }
        public string CategoryName { get; set; }
        public List<string> Images { get; set; }
        public int PromotionRate { get; set; }
        public bool IsPromoted { get; set; }
        public DateTime PromotionEndDate { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid CategoryId { get; set; }
        public ProductResponseModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Title = product.Title;
            Description = product.Description;
            AdditionalDescription = product.AdditionalDescription;
            Image = product.Image;
            StartRate = product.StartRate;
            SellingPrice = product.SellingPrice;
            CurrencyName = product.Currency?.Name;
            CategoryName = product.Category?.Name;
            CurrencyId = product.CurrencyId;
            CategoryId = product.CategoryId;

            if(Images != null)
                Images = product.Images.Select(x => x.Image).ToList();
        }
    }
}
