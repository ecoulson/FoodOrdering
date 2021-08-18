using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Ordering.Tests.Invoice
{
    using Ordering.Invoice;
    using Ordering.Order;

    public class InvoiceTests
    {
        [Fact]
        public void WHEN_CreatingAnInvoice_SHOULD_HaveExcpectedValues()
        {
            var mockOrderId = new Mock<IOrderId>();
            var mockTotal = new Mock<ITotal>();

            var invoice = new Invoice(
                mockOrderId.Object,
                new List<IInvoiceItem>(),
                mockTotal.Object
            );

            Xunit.Assert.NotNull(invoice.OrderId);
            Xunit.Assert.NotNull(invoice.Items);
            Xunit.Assert.NotNull(invoice.Total);
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullOrderId_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockTotal = new Mock<ITotal>();
                new Invoice(null, new List<IInvoiceItem>(), mockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullList_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockOrderId = new Mock<IOrderId>();
                var mockTotal = new Mock<ITotal>();
                new Invoice(mockOrderId.Object, null, mockTotal.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnInvoiceWithNullTotal_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                var mockOrderId = new Mock<IOrderId>();
                new Invoice(mockOrderId.Object, new List<IInvoiceItem>(), null);
            });
        }
    }
}
