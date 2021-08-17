using System;
using System.Collections.Generic;
using Menu;
using Moq;
using Xunit;


namespace Ordering.Tests
{
    public class OrderTests
    {
        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithNoItems_SHOULD_ReturnAnInvoiceWithNoCostOrItems()
        {
            var orderId = new Mock<IOrderId>().Object;
            var order = new Order(
                orderId,
                OrderState.WaitingForPayment,
                new List<IOrderItem>()
            );

            var invoice = order.GetInvoice();

            Xunit.Assert.Equal(0, invoice.Total.Cost);
            Xunit.Assert.Equal(orderId, invoice.OrderId);
            Xunit.Assert.Empty(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithAnItem_SHOULD_ReturnAnInvoiceWithCostAndItems()
        {
            var mockOrderId = new Mock<IOrderId>();
            var mockOrderItem = GetMockOrderItem(2, 500);

            var order = new Order(
                mockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<IOrderItem> { mockOrderItem.Object }
            );

            var invoice = order.GetInvoice();

            Xunit.Assert.Equal(1000, invoice.Total.Cost);
            Xunit.Assert.Single(invoice.Items);
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullItems_SHOULD_ThrowAnException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockOrderId = new Mock<IOrderId>();
                new Order(mockOrderId.Object, OrderState.WaitingForPayment, null);
            });
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullId_SHOULD_ThrowAnException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(null, OrderState.WaitingForPayment, new List<IOrderItem>());
            });
        }

        private Mock<IOrderItem> GetMockOrderItem(int amount, int cost)
        {
            var mockOrderItem = new Mock<IOrderItem>();
            mockOrderItem
                .Setup(item => item.MenuItem.Cost)
                .Returns(cost);
            mockOrderItem
                .Setup(item => item.Quantity.Value)
                .Returns(amount);
            return mockOrderItem;
        }
    }
}
