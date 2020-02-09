using DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<EntityEntry<Category>> AddCategory(Category category);
        Task RemoveCategory(Guid id);
    }
}
