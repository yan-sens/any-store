using Common.Models;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
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
        public async Task<List<ProductResponseModel>> GetAllProducts()
        {
            var result = await _dbContext.Products.Include(x => x.Currency).Include(x => x.Category).Select(x => new ProductResponseModel(x)).ToListAsync();
            return result;
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            var result = await _dbContext.Products.Include(x => x.Images).Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == productId);
            if (result != null)
            {
                result.Currency.Products = null;
                result.CurrencyName = result.Currency.Name;
                result.CurrencyDisplay = result.Currency.Display;
            }
            if (result.Category != null)
            {
                result.CategoryName = result.Category.Name;
                result.Category.Products = null;
            }               

            return result;
        }

        public async Task<List<string>> GetProductImagesByProductId(Guid productId)
        {
            return await _dbContext.ProductImages.Where(x => x.ProductId == productId).Select(x => x.Image).ToListAsync();
        }

        public async Task<List<ProductResponseModel>> GetProductsByCategoryId(Guid categoryId)
        {
            var result = new List<ProductResponseModel>();
            var products = await _dbContext.Products.Include(x => x.Currency).Where(x => x.CategoryId == categoryId).ToListAsync();
            var promotions = await _dbContext.PromotionMappings
                                                .Include(x => x.Promotion)
                                                .Where(x => x.Promotion.EndDate > DateTime.Now && x.Promotion.StartDate <= DateTime.Now)
                                                .ToListAsync();

            result = products.Select(x => new ProductResponseModel(x)).ToList();
            result.ForEach(x =>
            {
                var promotion = promotions.FirstOrDefault(p => p.ProductId == x.Id);
                if(promotion != null)
                {
                    x.IsPromoted = true;
                    x.PromotionRate = promotion.Promotion.Rate;
                    x.PromotionEndDate = promotion.Promotion.EndDate ?? DateTime.Now.AddHours(27);
                }
            });

            return result;
        }

        public async Task<List<ProductResponseModel>> GetPromotedProducts(int? count)
        {
            var result = new List<ProductResponseModel>();

            var query = _dbContext.PromotionMappings
                                        .Include(x => x.Promotion)
                                        .Include(x => x.Product)
                                        .Where(x => x.Promotion.EndDate > DateTime.Now && x.Promotion.StartDate <= DateTime.Now)
                                        .AsQueryable();

            if (count != null && count > 0)
                query = query.Take((int)count);

            var promotions = await query.ToListAsync();

            promotions.ForEach(x => {
                var response = new ProductResponseModel(x.Product);
                response.IsPromoted = true;
                response.PromotionRate = x.Promotion.Rate;
                response.PromotionEndDate = x.Promotion.EndDate ?? DateTime.Now.AddHours(27);

                if (!result.Any(r => r.Id == response.Id))
                    result.Add(response);
            });

            return result;
        }

        public async Task<List<PromotionMapping>> GetPromotionProducts()
        {
            var result = await _dbContext.PromotionMappings.Include(x => x.Product).ToListAsync();
            RemoveRecursion(ref result);
            return result;
        }

        public async Task CreateProduct(SaveProductModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                AdditionalDescription = model.AdditionalDescription,
                CategoryId = model.CategoryId,
                CurrencyId = model.CurrencyId,
                SellingPrice = model.SellingPrice,
                Image = model.Image,
                CreateDate = DateTime.Now
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            if (model.Images != null)
            {
                foreach (var _image in model.Images)
                {
                    ProductImage pi = new ProductImage { ProductId = product.Id, Image = _image };
                    _dbContext.ProductImages.Add(pi);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(SaveProductModel model)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (product == null)
                return;

            product.Name = model.Name;
            product.Title = model.Title;
            product.Description = model.Description;
            product.AdditionalDescription = model.AdditionalDescription;
            product.CategoryId = model.CategoryId;
            product.CurrencyId = model.CurrencyId;
            product.SellingPrice = model.SellingPrice;
            product.Image = model.Image;

            _dbContext.Update(product);

            var images = await _dbContext.ProductImages.Where(x => x.ProductId == model.Id).ToListAsync();
            foreach (var img in images)
                _dbContext.Remove(img);

            if (model.Images != null)
            {
                foreach (var _image in model.Images)
                {
                    ProductImage pi = new ProductImage { ProductId = product.Id, Image = _image };
                    _dbContext.ProductImages.Add(pi);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProduct(Guid id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            var productImages = await _dbContext.ProductImages.Where(x => x.ProductId == id).ToListAsync();

            if (product != null)
                _dbContext.Remove(product);

            if (productImages != null && productImages.Count > 0)
                productImages.ForEach(x => _dbContext.Remove(x));

            await _dbContext.SaveChangesAsync();
        }

        private void RemoveRecursion(ref List<PromotionMapping> products)
        {
            products.ForEach(x => {
                if (x.Product.Currency != null)
                {
                    x.Product.Currency.Products = null;
                    x.Product.CurrencyName = x.Product.Currency.Name;
                    x.Product.CurrencyDisplay = x.Product.Currency.Display;
                }
                if (x.Product.Category != null)
                {
                    x.Product.CategoryName = x.Product.Category.Name;
                    x.Product.Category = null;
                }
                x.Product.PromotionMappings = null;
            });
        }
    }
}
