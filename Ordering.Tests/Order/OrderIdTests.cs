using System;
using Ordering.Order;

namespace Ordering.Tests.Order
{
    using Xunit;

    public class OrderIdTests
    {
        [Fact]
        public void WHEN_OrderIdIsCreatedWithAnGUID_SHOULD_HaveTheSameGUID()
        {
            var id = new OrderId();

            Assert.NotEmpty(id.Value);
        }

        [Fact]
        public void WHEN_OrderIdIsCreatedWithANullOrderId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderId(null);
            });
        }
    }
}
