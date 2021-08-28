using System;
using Moq;

namespace Ordering.Tests.Invoice
{
    using Ordering.Invoice;
    using Ordering.Order;
    using Xunit;

    public class TotalTests
    {
        [Fact]
        public void WHEN_CreatingATotalWithNegativeAmount_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Total(-1);
            });
        }

        [Fact]
        public void WHEN_AddingAnOrderItemToTheTotal_SHOULD_UpdateTheTotal()
        {
            var total = Total.Zero();
            var mockOrderItem = getMockOrderItem(2, 5);

            total.AddToTotal(mockOrderItem.Object);

            Assert.Equal(10, total.Cost);
        }

        [Fact]
        public void WHEN_AddingANullOrderItem_SHOULD_ThrowAnException()
        {
            var total = Total.Zero();

            Assert.Throws<ArgumentNullException>(() =>
            {
                total.AddToTotal(null);
            });
        }

        [Fact]
        public void WHEN_AddingAnInvalidOrderItem_SHOULD_ThrowAnException()
        {
            var total = Total.Zero();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                total.AddToTotal(getMockOrderItem(1, -5).Object);
            });
        }

        [Fact]
        public void WHEN_CreatingATotalWithZeroValue_SHOULD_CreateAZeroTotal()
        {
            Assert.Equal(0, Total.Zero().Cost);
        }

        [Fact]
        public void WHEN_CreatingATotalFromAnOrderItem_SHOULD_HaveTheCorrectTotalForTheOrderItem()
        {
            var total = Total.FromOrderItem(getMockOrderItem(2, 5).Object);

            Assert.Equal(10, total.Cost);
        }

        private Mock<IOrderItem> getMockOrderItem(int amount, int cost)
        {
            var mockOrderItem = new Mock<IOrderItem>();
            mockOrderItem.Setup(orderItem => orderItem.Cost).Returns(amount * cost);
            return mockOrderItem;
        }
    }
}
