using Azure;
using Azure.Data.Tables;

namespace AdvaniaTest.Func.Entities;

public class ProductEntity : ITableEntity
{
    public required string PartitionKey { get; set; }
    public required string RowKey { get; set; } // RowKey == Row ID
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public double ProductPrice { get; set; }
    public int ProductStock { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
