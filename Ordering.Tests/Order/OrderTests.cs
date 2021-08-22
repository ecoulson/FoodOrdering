using System;
using System.Collections.Generic;
using Moq;


namespace Ordering.Tests.Order
{
    using Ordering.Order;
    using Xunit;

    public class OrderTests
    {
        private readonly Mock<IOrderId> mockOrderId;

        public OrderTests()
        {
            mockOrderId = new Mock<IOrderId>();
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithNoItems_SHOULD_ReturnAnInvoiceWithNoCostOrItems()
        {
            var order = new Order(
                mockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<IOrderItem>()
            );

            var invoice = order.GetInvoice();

            Assert.Equal(0, invoice.Total.Cost);
            Assert.Equal(mockOrderId.Object, invoice.OrderId);
            Assert.Empty(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithAnItem_SHOULD_ReturnAnInvoiceWithCostAndItems()
        {
            var mockOrderItem = GetMockOrderItem(2, 500);

            var order = new Order(
                mockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<IOrderItem> { mockOrderItem.Object }
            );

            var invoice = order.GetInvoice();

            Assert.Equal(1000, invoice.Total.Cost);
            Assert.Single(invoice.Items);
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullItems_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(mockOrderId.Object, OrderState.WaitingForPayment, null);
            });
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(null, OrderState.WaitingForPayment, new List<IOrderItem>());
            });
        }

        private Mock<IOrderItem> GetMockOrderItem(int amount, int cost)
        {
            var mockOrderItem = new Mock<IOrderItem>();
            mockOrderItem
                .Setup(orderItem => orderItem.Cost())
                .Returns(amount * cost);
            return mockOrderItem;
        }
    }
}
