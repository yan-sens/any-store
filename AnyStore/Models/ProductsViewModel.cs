using Common.Classes;
using Common.ViewModels;
using DAL.Models;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class ProductsViewModel : BaseViewModel
    {
        public Category Category { get; set; }

        public ProductsViewModel(List<CategoryMenuItem> menuItems, Category category)
            : base(menuItems)
        {
            Category = category;
        }
    }
}
