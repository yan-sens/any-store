using Common.Classes;
using DAL.Models;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class ProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ProductViewModel(List<CategoryMenuItem> menuItems, Product product)
            : base(menuItems)
        {
            Product = product;
        }
    }
}
