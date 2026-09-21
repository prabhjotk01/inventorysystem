using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Tests
{
    public class InventoryOrderServiceTests
    {
        [Fact]
        public void ProcessOrder_ValidOrder_ReturnsSuccess()
        {
            // Arrange
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "P001",
                Name = "Coffee",
                UnitPrice = 10.00m,
                StockQuantity = 20
            };

            service.AddProduct(product);

            // Act
            OrderResult result = service.ProcessOrder("P001", 2, 0.05m);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Order processed successfully.", result.Message);
        }
    }
}
