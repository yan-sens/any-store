using Common.Classes;
using Services.Models;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class BaseViewModel
    {
        public List<CategoryMenuItem> CategoryMenuItems { get; set; } = new List<CategoryMenuItem>();

        public List<ProductResponseModel> PromotedProducts { get; set; } = new List<ProductResponseModel>();

        public BaseViewModel(List<CategoryMenuItem> categoryMenuItems, List<ProductResponseModel> promotedProducts)
        {
            CategoryMenuItems = categoryMenuItems;
            PromotedProducts = promotedProducts;
        }
    }
}
