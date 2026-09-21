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
            
            InventoryOrderService service = new InventoryOrderService();

            Product product = new Product
            {
                Id = "0110",
                Name = "Hot Tea",
                UnitPrice = 10.00m,
                StockQuantity = 15
            };

            service.AddProduct(product);

            OrderResult result = service.ProcessOrder("0110", 15, 10.00m);

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

            OrderResult result = service.ProcessOrder("0102", 15, 10.00m);

            Assert.True(result.IsSuccess);
            Assert.Equal(15, product.StockQuantity);
        }
    }
}
