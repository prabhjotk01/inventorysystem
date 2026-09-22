using InventorySystem;
using Xunit;

namespace InventorySystem.Tests
{
    public class InventoryOrderServiceTests
    {
        [Fact]
        public void ProcessOrder_ValidOrder_ReturnsSuccess()
        {
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0110",
                Name = "Hot Tea",
                UnitPrice = 10.00m,
                StockQuantity = 15
            };

            service.AddProduct(product);

            
            OrderResult result = service.ProcessOrder("0110", 15, 0.10m);

            Assert.True(result.IsSuccess);
            Assert.Equal("Order processed successfully.", result.Message);
        }

        [Fact]
        public void ProcessOrder_ValidOrder_DeductsStock()
        {
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0102",
                Name = "Burger",
                UnitPrice = 8.00m,
                StockQuantity = 20
            };

            service.AddProduct(product);

            OrderResult result = service.ProcessOrder("0102", 15, 0.10m);

            Assert.True(result.IsSuccess);
            Assert.Equal(5, product.StockQuantity);
        }

        [Fact]
        public void ProcessOrder_ValidOrder_CalculatesTotalCost()
        {
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0103",
                Name = "HotWings",
                UnitPrice = 10.00m,
                StockQuantity = 20
            };

            service.AddProduct(product);

            OrderResult result = service.ProcessOrder("0103", 2, 0.05m);

            Assert.True(result.IsSuccess);
            Assert.Equal(21.00m, result.TotalCost);
        }

        [Fact]
        public void ProcessOrder_ZeroQuantity_ReturnsSuccess()
        {
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0104",
                Name = "Fries",
                UnitPrice = 5.00m,
                StockQuantity = 10
            };

            service.AddProduct(product);

            OrderResult result = service.ProcessOrder("0104", 0, 0.05m);

            Assert.True(result.IsSuccess);
            Assert.Equal(0.00m, result.TotalCost);
        }

        [Theory]
        [InlineData(10, 90.00)]
        [InlineData(50, 400.00)]
        public void ProcessOrder_DiscountBoundaries_CalculatesCorrectTotal(int quantity, decimal expectedTotal)
        {
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0105",
                Name = "Chicken",
                UnitPrice = 10.00m,
                StockQuantity = 60
            };

            service.AddProduct(product);

            OrderResult result = service.ProcessOrder("0105", quantity, 0.00m);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTotal, result.TotalCost);
        }
    }
}