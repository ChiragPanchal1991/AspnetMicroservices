using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext catalogContext)
        {
            this._context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public Task CreateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            //return await _context
            //     .Products
            //     .Find(p => p.Id == id)
            //     .FirstOrDefaultAsync();

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            return await _context
                            .Products
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            //return await _context
            //    .Products
            //    .Find(p => p.Category == categoryName)
            //    .ToListAsync();

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            //return await _context
            //    .Products
            //    .Find(p => p.Name == name)
            //    .ToListAsync();

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);

            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }
    }
}
