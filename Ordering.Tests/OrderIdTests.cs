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

            Assert.IsType<Guid>(id.Value);
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
