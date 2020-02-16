using Common.Classes;
using System.Collections.Generic;

namespace AnyStore.Models
{
    public class BaseViewModel
    {
        public List<CategoryMenuItem> CategoryMenuItems { get; set; } = new List<CategoryMenuItem>();

        public BaseViewModel(List<CategoryMenuItem> categoryMenuItems)
        {
            CategoryMenuItems = categoryMenuItems;
        }
    }
}
