using MongoDB.Driver;
using Catalog.Api.Data;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public class ProductRepository(ICatalogContext catalogContext) : IProductRepository
{
    private readonly ICatalogContext catalogContext = catalogContext;

    public async Task CreateProduct(Product product)
    {
        await catalogContext.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
        var deleteResult =
            await catalogContext.Products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
              && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await catalogContext.Products.Find(x => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategory(string category)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Category, category);
        return await catalogContext.Products.Find(filter).ToListAsync();
    }

    public async Task<Product> GetById(string id)
    {
        return await catalogContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
        return await catalogContext.Products.Find(filter).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult =
            await catalogContext.Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);

        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }
}