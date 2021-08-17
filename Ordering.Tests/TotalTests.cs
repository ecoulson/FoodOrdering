using System;
using Xunit;

namespace Ordering.Tests
{
    public class TotalTests
    {
        [Fact]
        public void WHEN_CreatingATotalWithNegativeAmount_SHOULD_ThrowException()
        {
            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Total(-1);
            });
        }
    }
}
