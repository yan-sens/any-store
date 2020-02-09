using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AnyStoreContext _dbContext;

        public CategoryService(AnyStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<EntityEntry<Category>> AddCategory(Category category)
        {
            return await _dbContext.Categories.AddAsync(category);
        }

        public async Task RemoveCategory(Guid id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }                
        }
    }
}
