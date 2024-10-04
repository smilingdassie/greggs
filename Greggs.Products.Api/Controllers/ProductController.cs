using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private const decimal GBP_TO_EUR_RATE = 1.11m; // Exchange rate


    private readonly IDataAccess<Product> _productAccess;  // Use ProductAccess or IDataAccess<Product>
    private readonly ILogger<ProductController> _logger;

    public ProductController(IDataAccess<Product> productAccess, ILogger<ProductController> logger)
    {
        _productAccess = productAccess;
        _logger = logger;
    }


    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        // Fetch products from the database with pagination
        var products = _productAccess.List(pageStart, pageSize);

        if (products == null || !products.Any())
        {
            _logger.LogWarning("No products found for the given page.");
            return new List<Product>(); // Return an empty list if no products are found
        }

        return products;
    }

    // New endpoint to return products with prices in Euros
        [HttpGet("euros")]
    public IEnumerable<ProductInEuros> GetProductsInEuros(int pageStart = 0, int pageSize = 5)
    {
        // Fetch products
        var products = _productAccess.List(pageStart, pageSize);

        if (products == null || !products.Any())
        {
            _logger.LogWarning("No products found for the given page.");
            return new List<ProductInEuros>();
        }

        // Convert prices to Euros and return DTO
        var productsInEuros = products.Select(p => new ProductInEuros
        {
            Id = p.Id,
            Name = p.Name,
            PriceInEuros = p.PriceInPounds * GBP_TO_EUR_RATE  // Convert price to Euros
        }).ToList();

        return productsInEuros;
    }
}
