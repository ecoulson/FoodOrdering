using System;
using Menu;
using Moq;
using Ordering.Order;

namespace Ordering.Tests.Order
{
    using Xunit;

    public class OrderItemTests
    {
        private Mock<IMenuItem> MockMenuItem;
        private Mock<IQuantity> MockQuantity;

        public OrderItemTests()
        {
            MockMenuItem = new Mock<IMenuItem>();
            MockQuantity = new Mock<IQuantity>();
        }

        [Fact]
        public void WHEN_OrderItemIsCreated_SHOULD_HaveExcepectedValues()
        {
            var orderItem = new OrderItem(MockMenuItem.Object, MockQuantity.Object);

            Assert.NotNull(orderItem.Quantity);
            Assert.NotNull(orderItem.MenuItem);
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullMenuItem_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(null, MockQuantity.Object);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNegativeQuantity_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(MockMenuItem.Object, null);
            });
        }
    }
}
