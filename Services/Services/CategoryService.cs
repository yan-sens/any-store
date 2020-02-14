using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _dbContext.Categories.Include(x => x.ParentCategory).ToListAsync();
        }
        public async Task<List<Category>> GetCategoriesWithChild()
        {
            return await _dbContext.Categories.Include(x => x.ParentCategory).Where(x => x.HasChildren).ToListAsync();
        }

        public async Task AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            _dbContext.SaveChanges();
        }

        public async Task UpdateCategory(Category category)
        {
            var _category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            _category.Name = category.Name;
            _category.Title = category.Title;
            _category.HasChildren = category.HasChildren;
            _category.Description = category.Description;
            _category.ParentCategoryId = category.ParentCategoryId;
            _dbContext.Entry(_category);
            await _dbContext.SaveChangesAsync();
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
