using System;
using Menu;
using Xunit;

namespace Ordering.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void WHEN_OrderItemIsCreated_SHOULD_HaveExcepectedValues()
        {
            var orderItem = new OrderItem(new MenuItem("", "", 2), 3);

            Assert.Equal(3, orderItem.Quantity);
            Assert.NotNull(orderItem.MenuItem);
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullMenuItem_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(null, 3);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNegativeQuantity_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new OrderItem(new MenuItem("", "", 2), -2);
            });
        }
    }
}
