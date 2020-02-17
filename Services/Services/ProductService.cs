using Common.Models;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
                CreateDate = DateTime.Now
            };

            if (model.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Image.Length);
                }
                product.Image = imageData;
            }

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            if (model.Images != null)
            {
                foreach (var _image in model.Images)
                {
                    ProductImage pi = new ProductImage { PatientId = product.Id };
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image.Length);
                    }
                    pi.Image = imageData;

                    _dbContext.ProductImages.Add(pi);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
