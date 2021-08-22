using System;
using System.Collections.Generic;
using Moq;

namespace Ordering.Tests.Invoice
{
    using Ordering.Invoice;
    using Ordering.Order;
    using Xunit;

    public class InvoiceTests
    {
        private readonly Mock<ITotal> mockTotal;
        private readonly Mock<IOrderId> mockOrderId;

        public InvoiceTests()
        {
            mockTotal = new Mock<ITotal>();
            mockOrderId = new Mock<IOrderId>();
        }

        [Fact]
        public void WHEN_CreatingAnInvoice_SHOULD_HaveExcpectedValues()
        {
            var invoice = new Invoice(
                mockOrderId.Object,
                new List<IInvoiceItem>(),
                mockTotal.Object
            );

            Assert.NotNull(invoice.OrderId);
            Assert.NotNull(invoice.Items);
            Assert.NotNull(invoice.Total);
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullOrderId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Invoice(null, new List<IInvoiceItem>(), mockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullList_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Invoice(mockOrderId.Object, null, mockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullTotal_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Invoice(mockOrderId.Object, new List<IInvoiceItem>(), null);
            });
        }
    }
}
