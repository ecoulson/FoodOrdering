using System;
using Xunit;

namespace Ordering.Tests
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
