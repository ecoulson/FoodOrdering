using System;
using System.Collections.Generic;
using Moq;


namespace Ordering.Tests.Order
{
    using Ordering.Order;
    using Payments;
    using Xunit;

    public class OrderTests
    {
        private Mock<IOrderId> MockOrderId;
        private Mock<IPaymentMethod> MockPaymentMethod;

        public OrderTests()
        {
            MockOrderId = new Mock<IOrderId>();
            MockPaymentMethod = new Mock<IPaymentMethod>();
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithNoItems_SHOULD_ReturnAnInvoiceWithNoCostOrItems()
        {
            var order = new Order(
                MockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<IOrderItem>(),
                new Mock<IPaymentMethod>().Object
            );

            var invoice = order.GetInvoice();

            Assert.Equal(0, invoice.Total.Cost);
            Assert.Equal(MockOrderId.Object, invoice.OrderId);
            Assert.Empty(invoice.Items);
        }

        [Fact]
        public void WHEN_InvoiceIsCreatedForAnOrderWithAnItem_SHOULD_ReturnAnInvoiceWithCostAndItems()
        {
            var mockOrderItem = GetMockOrderItem(2, 500);

            var order = new Order(
                MockOrderId.Object,
                OrderState.WaitingForPayment,
                new List<IOrderItem> { mockOrderItem.Object },
                new Mock<IPaymentMethod>().Object
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
                new Order(MockOrderId.Object, OrderState.WaitingForPayment, null, MockPaymentMethod.Object);
            });
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(null, OrderState.WaitingForPayment, new List<IOrderItem>(), MockPaymentMethod.Object);
            });
        }

        [Fact]
        public void WHEN_OrderIsCreatedWithNullPaymentMethod_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Order(MockOrderId.Object, OrderState.WaitingForPayment, new List<IOrderItem>(), null);
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
