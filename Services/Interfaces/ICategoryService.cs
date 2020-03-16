using Common.Classes;
using DAL.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseModel>> GetCategories();
        Task<List<CategoryResponseModel>> GetCategoriesWithChild();
        Task<List<CategoryResponseModel>> GetCategoriesForProduct();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task<Category> GetCategoryById(Guid categoryId);
        Task RemoveCategory(Guid id);
        Task<List<CategoryMenuItem>> GetCategoryMenuItems();
    }
}
