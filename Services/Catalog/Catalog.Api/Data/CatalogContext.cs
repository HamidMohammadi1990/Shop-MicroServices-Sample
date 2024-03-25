using MongoDB.Driver;
using Catalog.Api.Entities;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DataBasename"));

        Products = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeedData.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
}