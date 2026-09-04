using System.ComponentModel.DataAnnotations;

namespace AdvaniaTest.Func.DTOs;

public class Product
{
    //[Required]
    public required string ProductId { get; set; }
    //[Required]
    public required string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
}
