using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductRepository productRepository) : ControllerBase
{
    private readonly IProductRepository productRepository = productRepository;

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await productRepository.GetAll();
        return products;
    }

    [HttpGet("{id:length(24)}", Name = "GetById")]
    public async Task<Product> GetProductById(string id)
    {
        var product = await productRepository.GetById(id);
        return product;
    }

    [HttpGet("GetByCategory")]
    public async Task<IEnumerable<Product>> GetProductByCategory(string category)
    {
        var products = await productRepository.GetByCategory(category);
        return products;
    }

    [HttpPost("CreateProduct")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await productRepository.CreateProduct(product);
        return CreatedAtRoute("GetById", new { id = product.Id }, product);
    }

    [HttpPut("UpdateProduct")]
    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await productRepository.UpdateProduct(product);
        return result;
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    public async Task<bool> DeleteProduct(string id)
    {
        var result = await productRepository.DeleteProduct(id);
        return result;
    }
}