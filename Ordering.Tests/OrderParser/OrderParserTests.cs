namespace Ordering.Tests.OrderParser
{
    using Xunit;
    using Ordering.OrderParser;
    using Ordering.Service;
    using Moq;
    using System;
    using Ordering.Order;
    using System.Collections.Generic;

    public class OrderParserTests
    {
        private readonly Mock<IOrderLineParser> mockOrderLineParser;
        private readonly Mock<IText> mockText;
        private readonly OrderParser parser;

        public OrderParserTests()
        {
            mockOrderLineParser = new Mock<IOrderLineParser>();
            mockText = new Mock<IText>();
            parser = new OrderParser(
                mockOrderLineParser.Object
            );
        }

        [Fact]
        public void WHEN_ParsingAnEmptyText_SHOULD_ThrowException()
        {
            mockText
                .Setup(text => text.IsEmpty())
                .Returns(true);

            Assert.Throws<EmptyOrderException>(() =>
            {
                parser.Parse(mockText.Object);
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithANullText_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                parser.Parse(null);
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithOrderLines_SHOULD_ThrowAnException()
        {
            mockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Throws(new IllegalOrderLinesException(""));

            Assert.Throws<IllegalOrderLinesException>(() =>
            {
                parser.Parse(mockText.Object);
            });
            
            mockOrderLineParser.VerifyAll();
        }

        [Fact]
        public void WHEN_ParsingAValidOrder_SHOULD_ReturnAnOrder()
        {
            mockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(new List<IOrderItem>());

            var order = parser.Parse(mockText.Object);

            mockOrderLineParser.VerifyAll();
            Assert.Equal(OrderState.Created, order.State);
        }
    }
}
