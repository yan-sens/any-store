using Common.Classes;
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
        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<List<Category>> GetCategoriesWithChild()
        {
            return await _dbContext.Categories.Include(x => x.ParentCategory).Where(x => x.HasChildren).ToListAsync();
        }

        public async Task<List<CategoryMenuItem>> GetCategoryMenuItems()
        {
            var response = new List<CategoryMenuItem>();
            var allCategories = await _dbContext.Categories.ToListAsync();
            allCategories.Where(x => x.ParentCategoryId == null).ToList().ForEach(x => {
                var childItems = new List<CategoryMenuItem>();
                allCategories.Where(c => c.ParentCategoryId == x.Id).ToList().ForEach(c => {
                    var subChildItems = new List<CategoryMenuItem>();
                    allCategories.Where(ac => ac.ParentCategoryId == c.Id).ToList().ForEach(ac =>
                    {
                        subChildItems.Add(new CategoryMenuItem()
                        {
                            Id = ac.Id,
                            Name = ac.Name,
                            ChildItems = null
                        });
                    });

                    childItems.Add(new CategoryMenuItem()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ChildItems = subChildItems
                    });
                });

                response.Add(new CategoryMenuItem()
                {
                    Id = x.Id,
                    Name = x.Title,
                    ChildItems = childItems
                });
            });

            return response;
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
