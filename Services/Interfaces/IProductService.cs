using Common.Models;
using DAL.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetAllProducts();
        Task<List<ProductResponseModel>> GetProductsByCategoryId(Guid categoryId);
        Task<List<string>> GetProductImagesByProductId(Guid productId);
        Task<Product> GetProductById(Guid productId);
        Task CreateProduct(SaveProductModel model);
        Task UpdateProduct(SaveProductModel model);
        Task<List<PromotionMapping>> GetPromotionProducts();
        Task RemoveProduct(Guid id);
        Task<List<ProductResponseModel>> GetPromotedProducts(int? count);
    }
}
