using AdvaniaTest.Func.Entities;
using AdvaniaTest.Func.Interfaces;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;

namespace AdvaniaTest.Func.Repositories;

public class ProductRepository : IProductRepository
{
    private const string partitionKey = "PRODUCTS";
    private readonly TableClient _tableClient;
    private readonly ILogger<ProductRepository> _loggar;

    public ProductRepository(TableClient tableClient, ILogger<ProductRepository> logger)
    {
        _tableClient = tableClient;
        _loggar = logger;
    }
    public async Task<bool> AddProductAsync(ProductEntity entity)
    {
        try
        {
            var exists = await _tableClient.GetEntityIfExistsAsync<ProductEntity>(partitionKey, entity.RowKey);

            if(exists.HasValue is true)
            {
                _loggar.LogInformation("Product with Id : {id} already exists", entity.RowKey);
                return false;
            }
            
            var added = await _tableClient.AddEntityAsync(entity);

            if (added.IsError is false)
            {
                _loggar.LogInformation("Product with {id} added successfully", entity.RowKey);
                return true;
            }
            _loggar.LogWarning("Table Storage return status : {statusCode}. With Reason : {reason}", added.Status, added.ReasonPhrase);
            return false;
        }
        catch (Exception ex)
        {
            _loggar.LogError(ex, "Table Storage error on adding product. Exception Message : {message}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _tableClient.QueryAsync<ProductEntity>(e => e.PartitionKey == partitionKey).ToListAsync();
            _loggar.LogInformation("Products retrieved : {count}", entities.Count);
            return entities;
        }
        catch (Exception ex)
        {
            _loggar.LogError(ex, "Table Storage error on retrieving products. Exception Message : {message}", ex.Message);
            throw;
        }
    }
}
