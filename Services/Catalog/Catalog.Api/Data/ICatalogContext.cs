using MongoDB.Driver;
using Catalog.Api.Entities;

namespace Catalog.Api.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}