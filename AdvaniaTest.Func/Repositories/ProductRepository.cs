using AdvaniaTest.Func.Entities;
using AdvaniaTest.Func.Interfaces;
using Azure.Data.Tables;
using System.Net;

namespace AdvaniaTest.Func.Repositories;

public class ProductRepository : IProductRepository
{
    private const string partitionKey = "PRODUCTS";
    private readonly TableClient _tableClient;

    public ProductRepository(TableClient tableClient)
    {
        _tableClient = tableClient;
    }
    public async Task<bool> AddProductAsync(ProductEntity entity)
    {
        try
        {
            var exists = await _tableClient.GetEntityIfExistsAsync<ProductEntity>(partitionKey, entity.RowKey);

            if(exists is null)
            {
                return false;
            }

            var added = await _tableClient.AddEntityAsync(entity);

            if (added.Status == 200)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _tableClient.QueryAsync<ProductEntity>(e => e.PartitionKey == partitionKey).ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
