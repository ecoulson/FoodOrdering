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
            var orderId = new OrderId();
            var order = new Order(
                orderId,
                OrderState.WaitingForPayment,
                new List<OrderItem>()
            );

            var invoice = order.getInvoice();

            Assert.Equal(0, invoice.Total.Cost);
            Assert.Equal(orderId, invoice.OrderId);
            Assert.Empty(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithAnItem_SHOULD_ReturnAnInvoiceWithCostAndItems()
        {
            var mockOrderId = new Mock<OrderId>();
            var mockOrderItem = GetMockOrderItem(2, 500);

            var order = new Order(
                mockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<OrderItem> { mockOrderItem.Object }
            );

            var invoice = order.getInvoice();

            Assert.Equal(1000, invoice.Total.Cost);
            Assert.Equal(mockOrderId.Object, invoice.OrderId);
            Assert.Single(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedWithNullItems_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mockOrderId = new Mock<OrderId>();
                new Order(mockOrderId.Object, OrderState.WaitingForPayment, null);
            });
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedWithNullId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(null, OrderState.WaitingForPayment, new List<OrderItem>());
            });
        }

        private Mock<OrderItem> GetMockOrderItem(int amount, int cost)
        {
            var mockMenuItem = new Mock<MenuItem>("", "", cost);
            var mockQuantity = new Mock<Quantity>(amount);
            return new Mock<OrderItem>(mockMenuItem.Object, mockQuantity.Object);
        }
    }
}
