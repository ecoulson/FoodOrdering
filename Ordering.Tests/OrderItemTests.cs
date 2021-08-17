using System;
using Menu;
using Moq;
using Xunit;

namespace Ordering.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void WHEN_OrderItemIsCreated_SHOULD_HaveExcepectedValues()
        {
            var mockMenuItem = new Mock<MenuItem>("", "", 0);
            var mockQuantity = new Mock<Quantity>(2);
            var orderItem = new OrderItem(mockMenuItem.Object, mockQuantity.Object);

            Assert.NotNull(orderItem.Quantity);
            Assert.NotNull(orderItem.MenuItem);
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullMenuItem_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mockQuantity = new Mock<Quantity>(2);
                new OrderItem(null, mockQuantity.Object);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNegativeQuantity_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mockMenuItem = new Mock<MenuItem>("", "", 0);
                new OrderItem(mockMenuItem.Object, null);
            });
        }
    }
}
