using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
    public class CategoryResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasChildren { get; set; }
        public int Step { get; set; }
        public string ParentCategoryName { get; set; }
        public Category ParentCategory { get; set; }
        public List<ProductResponseModel> Products { get; set; }
        public CategoryResponseModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Title = category.Title;
            Description = category.Description;
            HasChildren = category.HasChildren;
            Step = category.Step;
            ParentCategory = category.ParentCategory;
            ParentCategoryName = category.ParentCategory?.Name;
            Products = category.Products?.Select(x => new ProductResponseModel(x)).ToList();

            if(ParentCategory != null)
                ParentCategory.Categories = null;
        }
    }
}
