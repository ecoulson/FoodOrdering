using System;
using Xunit;

namespace Ordering.Tests
{
    public class QuantityTests
    {
        [Fact]
        public void WHEN_CreatingAQuantityWithAPositiveAmount_SHOULD_HaveExpectedValues()
        {
            var quantity = new Quantity(3);

            Assert.Equal(3, quantity.Value);
        }

        [Fact]
        public void WHEN_CreatingAQuantityWithANonPositiveAmount_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Quantity(0);
            });
        }
    }
}
