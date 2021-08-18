using System;
using Menu;
using Moq;
using Ordering.Order;
using Xunit;

namespace Ordering.Tests.Order
{
    public class OrderItemTests
    {
        [Fact]
        public void WHEN_OrderItemIsCreated_SHOULD_HaveExcepectedValues()
        {
            var mockMenuItem = new Mock<IMenuItem>();
            var mockQuantity = new Mock<IQuantity>();
            var orderItem = new OrderItem(mockMenuItem.Object, mockQuantity.Object);

            Xunit.Assert.NotNull(orderItem.Quantity);
            Xunit.Assert.NotNull(orderItem.MenuItem);
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullMenuItem_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockQuantity = new Mock<IQuantity>();
                new OrderItem(null, mockQuantity.Object);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNegativeQuantity_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockMenuItem = new Mock<IMenuItem>();
                new OrderItem(mockMenuItem.Object, null);
            });
        }
    }
}
