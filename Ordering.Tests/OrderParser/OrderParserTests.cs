namespace Ordering.Tests.OrderParser
{
    using Xunit;
    using Ordering.OrderParser;
    using Ordering.Service;
    using Moq;
    using System;
    using Ordering.Order;
    using System.Collections.Generic;
    using Payments;

    public class OrderParserTests
    {
        private Mock<IOrderLineParser> MockOrderLineParser;
        private Mock<IText> MockText;
        private Mock<IOrderLineParseResult> MockOrderLineParserResult;
        private OrderParser Parser;

        public OrderParserTests()
        {
            MockOrderLineParser = new Mock<IOrderLineParser>();
            MockText = new Mock<IText>();
            MockOrderLineParserResult = new Mock<IOrderLineParseResult>();
            Parser = new OrderParser(
                MockOrderLineParser.Object
            );
        }

        [Fact]
        public void WHEN_ParsingAnEmptyText_SHOULD_ThrowException()
        {
            MockText
                .Setup(text => text.IsEmpty())
                .Returns(true);

            Assert.Throws<EmptyOrderException>(() =>
            {
                Parser.Parse(MockText.Object);
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithANullText_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Parser.Parse(null);
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithOrderLines_SHOULD_ThrowAnException()
        {
            MockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(MockOrderLineParserResult.Object);
            MockOrderLineParserResult
                .Setup(parserResult => parserResult.HasExceptions())
                .Returns(true);
            MockOrderLineParserResult
                .Setup(orderLineResult => orderLineResult.BuildException())
                .Returns(new IllegalOrderLineException(new string[0]));

            Assert.Throws<IllegalOrderLineException>(() =>
            {
                Parser.Parse(MockText.Object);
            });
            
            MockOrderLineParser.VerifyAll();
        }

        [Fact]
        public void WHEN_ParsingAValidOrder_SHOULD_ReturnAnOrder()
        {
            MockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(MockOrderLineParserResult.Object);
            MockOrderLineParserResult
                .Setup(orderLineResult => orderLineResult.Items)
                .Returns(new List<IOrderItem>());

            var order = Parser.Parse(MockText.Object);

            MockOrderLineParser.VerifyAll();
            Assert.Equal(OrderState.Created, order.State);
        }
    }
}
