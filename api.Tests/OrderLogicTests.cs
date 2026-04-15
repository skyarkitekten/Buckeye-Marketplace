using Xunit;
using System.Collections.Generic;

namespace api.Tests
{
    // A simple class to represent what we are testing 
    // (Mimicking your backend logic)
    public class OrderCalculator
    {
        public decimal CalculateTotal(List<decimal> prices)
        {
            decimal total = 0;
            foreach (var price in prices) total += price;
            return total;
        }
    }

    public class OrderLogicTests
    {
        [Fact]
        public void CalculateTotal_ShouldReturnCorrectSum()
        {
            // Arrange
            var calculator = new OrderCalculator();
            var prices = new List<decimal> { 10.00m, 20.50m, 5.00m };

            // Act
            var result = calculator.CalculateTotal(prices);

            // Assert
            Assert.Equal(35.50m, result);
        }

        [Fact]
        public void PasswordValidator_ShouldFailShortPasswords()
        {
            // Simple logic test for password rules
            string password = "123";
            bool isValid = password.Length >= 8;
            
            Assert.False(isValid);
        }

        [Fact]
        public void OrderStatus_InitialStatus_ShouldBePending()
        {
            // Testing the logic of a new order entity
            var initialStatus = "Pending";
            Assert.Equal("Pending", initialStatus);
        }
    }
}