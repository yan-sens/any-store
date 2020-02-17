using Common.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductsByCategoryId(Guid categoryId);
        Task<Product> GetProductById(Guid productId);
        Task CreateProduct(SaveProductModel model);
    }
}
