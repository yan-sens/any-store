using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasChildren { get; set; }
        public Guid? ParentCategoryId { get; set; }
        [NotMapped]
        public string ParentCategoryName { get; set; }
        [NotMapped]
        public List<Category> Categories { get; set; }
        [NotMapped]
        public Category ParentCategory { get; set; }
    }
}
