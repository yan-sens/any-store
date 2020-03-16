using Common.Classes;
using Common.ViewModels;
using DAL.Models;
using Services.Models;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class ProductsViewModel : BaseViewModel
    {
        public Category Category { get; set; }

        public ProductsViewModel(List<CategoryMenuItem> menuItems, List<ProductResponseModel> promotedProducts, Category category)
            : base(menuItems, promotedProducts)
        {
            Category = category;
        }
    }
}
