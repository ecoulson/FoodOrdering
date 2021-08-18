using System;
using Xunit;

namespace Ordering.Tests.OrderParser
{
    using Ordering.OrderParser;

    public class OrderParserTests
    {
        [Fact]
        public void WHEN_ParsingAnEmptyText_SHOULD_ThrowException()
        {
            var parser = new OrderParser();
        }
    }
}
