using System;

namespace AnyStore.Models
{
    public class CategoryRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasChildren { get; set; }
        public string ParentCategoryId { get; set; }
    }
}
