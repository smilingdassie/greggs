using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.DataAccess;
using Moq;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IDataAccess<Product>> _mockProductAccess;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _controller;

        private const decimal GBP_TO_EUR_RATE = 1.11m;

        public ProductControllerTests()
        {
            _mockProductAccess = new Mock<IDataAccess<Product>>();
            _mockLogger = new Mock<ILogger<ProductController>>();

            // Instantiate the controller with mocked dependencies
            _controller = new ProductController(_mockProductAccess.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsProducts_WithPagination()
        {
            // Arrange
            var mockProducts = GetSampleProducts();
            _mockProductAccess.Setup(x => x.List(0, 5)).Returns(mockProducts);

            // Act
            var result = _controller.Get(0, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());  // Assuming 5 products are returned
        }

        [Fact]
        public void Get_ReturnsEmptyList_IfNoProducts()
        {
            // Arrange
            _mockProductAccess.Setup(x => x.List(0, 5)).Returns(new List<Product>());

            // Act
            var result = _controller.Get(0, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);  // Expecting empty result
        }

        [Fact]
        public void GetProductsInEuros_ReturnsProducts_WithConvertedPrices()
        {
            // Arrange
            var mockProducts = GetSampleProducts();
            _mockProductAccess.Setup(x => x.List(0, 5)).Returns(mockProducts);

            // Act
            var result = _controller.GetProductsInEuros(0, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());

            // Allow a small tolerance for the comparison
            var tolerance = 0.01m;  // You can adjust this based on acceptable precision

            foreach (var product in result)
            {
                var originalProduct = mockProducts.First(p => p.Id == product.Id);
                var expectedPriceInEuros = originalProduct.PriceInPounds * GBP_TO_EUR_RATE;
                Assert.InRange(product.PriceInEuros, expectedPriceInEuros - tolerance, expectedPriceInEuros + tolerance);
            }
        }


        [Fact]
        public void GetProductsInEuros_ReturnsEmptyList_IfNoProducts()
        {
            // Arrange
            _mockProductAccess.Setup(x => x.List(0, 5)).Returns(new List<Product>());

            // Act
            var result = _controller.GetProductsInEuros(0, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Helper method to return sample products
        private List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Sausage Roll", PriceInPounds = 1.00m },
                new Product { Id = 2, Name = "Vegan Sausage Roll", PriceInPounds = 1.10m },
                new Product { Id = 3, Name = "Steak Bake", PriceInPounds = 1.20m },
                new Product { Id = 4, Name = "Yum Yum", PriceInPounds = 0.70m },
                new Product { Id = 5, Name = "Pink Jammie", PriceInPounds = 0.50m }
            };
        }
    }
}
