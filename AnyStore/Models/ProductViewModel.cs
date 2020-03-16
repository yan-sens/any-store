using Common.Classes;
using DAL.Models;
using Services.Models;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class ProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ProductViewModel(List<CategoryMenuItem> menuItems, List<ProductResponseModel> promotedProducts, Product product)
            : base(menuItems, promotedProducts)
        {
            Product = product;
        }
    }
}
