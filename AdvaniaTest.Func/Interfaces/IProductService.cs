using AdvaniaTest.Func.DTOs;

namespace AdvaniaTest.Func.Interfaces;

public interface IProductService
{
    Task<bool> AddProduct(Product product);
    Task<List<Product>> GetProductsAsync();
}
