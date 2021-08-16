using System;
using System.Collections.Generic;
using Menu;
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
            var orderId = new OrderId();

            var order = new Order(
                orderId,
                OrderState.WaitingForPayment,
                new List<OrderItem> { new OrderItem(new MenuItem("", "", 5), 1) }
            );

            var invoice = order.getInvoice();

            Assert.Equal(5, invoice.Total.Cost);
            Assert.Equal(orderId, invoice.OrderId);
            Assert.Single(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedWithNullItems_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(new OrderId(), OrderState.WaitingForPayment, null);
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
    }
}
