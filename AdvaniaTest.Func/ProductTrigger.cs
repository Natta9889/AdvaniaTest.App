using AdvaniaTest.Func.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AdvaniaTest.Func;

public class ProductTrigger
{
    private readonly ILogger<ProductTrigger> _logger;

    public ProductTrigger(ILogger<ProductTrigger> logger)
    {
        _logger = logger;
    }

    [Function("Add-Product")]
    public IActionResult RunAddProduct([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("POST Add Product Endpoint Triggered.");
            var product = req.ReadFromJsonAsync<Product>();

            return new OkObjectResult(product.Result);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [Function("Get-Products")]
    public IActionResult RunGetProducts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("GET Products Endpoint Triggered.");

        }
        catch (Exception ex)
        {

            throw;
        }
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}