using System;
using System.Collections.Generic;
using Moq;


namespace Ordering.Tests.Order
{
    using Ordering.Order;
    using Xunit;

    public class OrderTests
    {
        private static readonly string DummyOrderId = "a2e02668-5dca-477f-81b4-f0ca2b88042d";

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithNoItems_SHOULD_ReturnAnInvoiceWithNoCostOrItems()
        {
            var order = new Order(
                OrderState.WaitingForPayment,
                new List<IOrderItem>()
            );

            order.Id = new OrderId(DummyOrderId);
            var invoice = order.GetInvoice();

            Assert.Equal(0, invoice.Total.Cost);
            Assert.Empty(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithAnItem_SHOULD_ReturnAnInvoiceWithCostAndItems()
        {
            var mockOrderItem = GetMockOrderItem(2, 500);

            var order = new Order(
                OrderState.WaitingForPayment,
                new List<IOrderItem> { mockOrderItem.Object }
            );

            order.Id = new OrderId(DummyOrderId);
            var invoice = order.GetInvoice();

            Assert.Equal(1000, invoice.Total.Cost);
            Assert.Single(invoice.Items);
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullItems_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(OrderState.WaitingForPayment, null);
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
                .Setup(orderItem => orderItem.Cost)
                .Returns(amount * cost);
            return mockOrderItem;
        }

        [Fact]
        public void WHEN_SettingOrderId_SHOULD_SetId()
        {
            var order = new Order(OrderState.WaitingForPayment, new List<IOrderItem>());

            order.Id = new OrderId(DummyOrderId);

            Assert.Equal(DummyOrderId, order.Id.Value);
        }
    }
}
