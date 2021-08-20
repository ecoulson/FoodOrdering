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
        private Mock<ITotal> MockTotal;
        private Mock<IOrderId> MockOrderId;

        public InvoiceTests()
        {
            MockTotal = new Mock<ITotal>();
            MockOrderId = new Mock<IOrderId>();
        }

        [Fact]
        public void WHEN_CreatingAnInvoice_SHOULD_HaveExcpectedValues()
        {
            var invoice = new Invoice(
                MockOrderId.Object,
                new List<IInvoiceItem>(),
                MockTotal.Object
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
                new Invoice(null, new List<IInvoiceItem>(), MockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullList_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Invoice(MockOrderId.Object, null, MockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullTotal_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Invoice(MockOrderId.Object, new List<IInvoiceItem>(), null);
            });
        }
    }
}
