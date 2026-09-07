using AdvaniaTest.Func.DTOs;
using AdvaniaTest.Func.Entities;
using AdvaniaTest.Func.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdvaniaTest.Func.Services;

public class ProductService : IProductService
{
    private const string partitionKey = "PRODUCTS";
    private readonly IProductRepository _repo;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repo, ILogger<ProductService> logger) 
    {
        _repo = repo;
        _logger = logger;
    }
    public async Task<bool> AddProduct(Product product)
    {
        try
        {
            var entity = new ProductEntity
            {
                PartitionKey = partitionKey,
                RowKey = product.ProductId,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = (double)product.ProductPrice,
                ProductStock = product.ProductStock
            };
            return await _repo.AddProductAsync(entity);
            
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            var productEntities = await _repo.GetAllAsync();
            var products = new List<Product>();

            foreach (var entity in productEntities)
            {
                var product = new Product
                {
                    ProductId = entity.ProductId,
                    ProductName= entity.ProductName,
                    ProductDescription= entity.ProductDescription,
                    ProductPrice = (decimal)entity.ProductPrice,
                    ProductStock = entity.ProductStock
                };
                products.Add(product);
            }
            return products;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
