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
        private Mock<IPaymentMethodParser> MockPaymentMethodParser;
        private Mock<IText> MockText;
        private Mock<IParserResult<List<IOrderItem>>> MockOrderLineParserResult;
        private Mock<IParserResult<IPaymentMethod>> MockPaymentMethodParserResult;
        private OrderParser Parser;

        public OrderParserTests()
        {
            MockOrderLineParser = new Mock<IOrderLineParser>();
            MockPaymentMethodParser = new Mock<IPaymentMethodParser>();
            MockText = new Mock<IText>();
            MockOrderLineParserResult = new Mock<IParserResult<List<IOrderItem>>>();
            MockPaymentMethodParserResult = new Mock<IParserResult<IPaymentMethod>>();
            Parser = new OrderParser(
                MockOrderLineParser.Object,
                MockPaymentMethodParser.Object
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
                .Throws(new IllegalOrderLineException(new string[0]));       

            Assert.Throws<IllegalOrderLineException>(() =>
            {
                Parser.Parse(MockText.Object);
            });
            
            MockOrderLineParser.VerifyAll();
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithInvalidPaymentMethod_SHOULD_ThrowAnException()
        {
            MockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(MockOrderLineParserResult.Object);
            MockPaymentMethodParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Throws(new IllegalPaymentMethodException("bad payment method"));

            Assert.Throws<IllegalPaymentMethodException>(() =>
            {
                Parser.Parse(MockText.Object);
            });

            MockOrderLineParser.VerifyAll();
            MockPaymentMethodParser.VerifyAll();
        }

        [Fact]
        public void WHEN_ParsingAValidOrder_SHOULD_ReturnAnOrder()
        {
            MockOrderLineParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(MockOrderLineParserResult.Object);
            MockOrderLineParserResult
                .Setup(orderLineResult => orderLineResult.Value)
                .Returns(new List<IOrderItem>());
            MockPaymentMethodParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(MockPaymentMethodParserResult.Object);
            MockPaymentMethodParserResult
                .Setup(paymentMethodResult => paymentMethodResult.Value)
                .Returns(new Mock<IPaymentMethod>().Object);

            var order = Parser.Parse(MockText.Object);

            MockOrderLineParser.VerifyAll();
            MockPaymentMethodParser.VerifyAll();
            Assert.NotNull(order.PaymentMethod);
            Assert.Equal(OrderState.Created, order.State);
        }
    }
}
