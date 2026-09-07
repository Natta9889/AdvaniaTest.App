using AdvaniaTest.Func.DTOs;
using AdvaniaTest.Func.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AdvaniaTest.Func;

public class ProductTrigger
{
    private readonly ILogger<ProductTrigger> _logger;
    private readonly IProductService _service;

    public ProductTrigger(ILogger<ProductTrigger> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    [Function("addproduct")]
    public async Task<IActionResult> RunAddProduct([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("POST Add Product Endpoint Triggered.");
            var product = await req.ReadFromJsonAsync<Product>();
            if(product is null)
            {
                return new BadRequestResult();
            }
            var added = await _service.AddProduct(product);
            if(added is true)
            {
                return new CreatedResult();
            }
            return new BadRequestResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An Exception was Thrown.");
            return new ObjectResult("An internal server error occurred.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [Function("getproducts")]
    public async Task<IActionResult> RunGetProducts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("GET Products Endpoint Triggered.");
            var products = await _service.GetProductsAsync();
            return new OkObjectResult(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An Exception was Thrown.");
            return new ObjectResult("An internal server error occurred.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}