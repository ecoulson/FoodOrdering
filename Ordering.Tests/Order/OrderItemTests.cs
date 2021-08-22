using System;
using Moq;
using Ordering.Order;

namespace Ordering.Tests.Order
{
    using Ordering.OrderParser;
    using Xunit;

    public class OrderItemTests
    {
        private readonly Mock<IMenuItemIdentifier> mockMenuItemId;
        private readonly Mock<IQuantity> mockQuantity;
        private readonly Mock<ICost> mockCost;

        public OrderItemTests()
        {
            mockMenuItemId = new Mock<IMenuItemIdentifier>();
            mockQuantity = new Mock<IQuantity>();
            mockCost = new Mock<ICost>();
        }

        [Fact]
        public void WHEN_OrderItemIsCreated_SHOULD_HaveExcepectedValues()
        {
            var orderItem = new OrderItem(mockMenuItemId.Object, mockQuantity.Object, mockCost.Object);

            Assert.NotNull(orderItem);
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullMenuItem_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(null, mockQuantity.Object, mockCost.Object);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullQuantity_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(mockMenuItemId.Object, null, mockCost.Object);
            });
        }

        [Fact]
        public void WHEN_OrderItemIsCreatedWithNullCost_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderItem(mockMenuItemId.Object, mockQuantity.Object, null);
            });
        }

        [Fact]
        public void WHEN_CalculatingCost_SHOULD_ReturnCorrectCost()
        {
            mockQuantity
                .Setup(quantity => quantity.Value)
                .Returns(5);
            mockCost
                .Setup(cost => cost.Value)
                .Returns(200);

            var item = new OrderItem(mockMenuItemId.Object, mockQuantity.Object, mockCost.Object);

            Assert.Equal(1000, item.Cost());
        }
    }
}
