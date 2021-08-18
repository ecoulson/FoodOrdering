using System;
using Ordering.Order;
using Xunit;

namespace Ordering.Tests.Order
{
    public class OrderIdTests
    {
        [Fact]
        public void WHEN_OrderIdIsCreatedWithAnGUID_SHOULD_HaveTheSameGUID()
        {
            var id = new OrderId();

            Xunit.Assert.IsType<Guid>(id.Value);
        }

        [Fact]
        public void WHEN_OrderIdIsCreatedWithANullOrderId_SHOULD_ThrowAnException()
        {
            Xunit.Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderId(null);
            });
        }
    }
}
