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
    public class ProductService : IProductService
    {
        private readonly AnyStoreContext _dbContext;

        public ProductService(AnyStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<List<Product>> GetProductsByCategoryId(Guid categoryId)
        {
            return await _dbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
