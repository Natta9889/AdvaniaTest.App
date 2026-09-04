using System.ComponentModel.DataAnnotations;

namespace AdvaniaTest.Func.DTOs;

public class Product
{
    [Required]
    public string ProductId { get; set; }
    [Required]
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
}
