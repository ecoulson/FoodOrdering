using System;
using Ordering.Order;
using Xunit;

namespace Ordering.Tests.Order
{
    public class QuantityTests
    {
        [Fact]
        public void WHEN_CreatingAQuantityWithAPositiveAmount_SHOULD_HaveExpectedValues()
        {
            var quantity = new Quantity(3);

            Xunit.Assert.Equal(3, quantity.Value);
        }

        [Fact]
        public void WHEN_CreatingAQuantityWithANonPositiveAmount_SHOULD_ThrowAnException()
        {
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Quantity(0);
            });
        }
    }
}
