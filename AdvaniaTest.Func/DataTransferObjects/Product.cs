namespace AdvaniaTest.Func.DTOs;

public class Product
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
}
