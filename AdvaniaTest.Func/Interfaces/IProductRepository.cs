using AdvaniaTest.Func.Entities;

namespace AdvaniaTest.Func.Interfaces;

public interface IProductRepository
{
    Task<bool> AddProductAsync(ProductEntity entity);
    Task<IEnumerable<ProductEntity>> GetAllAsync();
}
