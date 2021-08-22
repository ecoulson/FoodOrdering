namespace Ordering.Tests.OrderParser
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Ordering.Order;
    using Ordering.OrderParser;
    using Ordering.Service;
    using Xunit;

    public class OrderLineParserTests
    {
        private readonly Mock<IText> mockText;
        private readonly Mock<IOrderLineExtractor> mockOrderLineExtractor;
        private readonly Mock<IOrderLineValidator> mockOrderLinesValidator;
        private readonly Mock<IOrderLineMatcher> mockOrderLineMatcher;
        private readonly Mock<IOrderItem> mockOrderItem;
        private readonly IOrderLineParser parser;

        public OrderLineParserTests()
        {
            mockText = new Mock<IText>();
            mockOrderLineExtractor = new Mock<IOrderLineExtractor>();
            mockOrderLinesValidator = new Mock<IOrderLineValidator>();
            mockOrderLineMatcher = new Mock<IOrderLineMatcher>();
            mockOrderItem = new Mock<IOrderItem>();
            parser = new OrderLineParser(
                mockOrderLineExtractor.Object,
                mockOrderLinesValidator.Object,
                mockOrderLineMatcher.Object
            );
        }

        [Fact]
        public void WHEN_CreatingAParserWithANullExtractor_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderLineParser(
                    null,
                    mockOrderLinesValidator.Object,
                    mockOrderLineMatcher.Object
                );
            });
        }

        [Fact]
        public void WHEN_CreatingAParserWithANullValidator_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderLineParser(
                    mockOrderLineExtractor.Object,
                    null,
                    mockOrderLineMatcher.Object
                );
            });
        }

        [Fact]
        public void WHEN_CreatingAParserWithANullMatcher_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderLineParser(
                    mockOrderLineExtractor.Object,
                    mockOrderLinesValidator.Object,
                    null
                );
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderLineWithANullText_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                parser.Parse(null);
            });
        }

        [Fact]
        public void WHEN_ParsingAnOrderWithText_SHOULD_ReturnAListOfOrderItems()
        {
            mockOrderLineExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Returns(new List<string> { "order line", "order line" });
            mockOrderLineMatcher
                .Setup(matcher => matcher.Match(It.IsAny<string>()))
                .Returns(mockOrderItem.Object);
            mockText
                .Setup(text => text.Content)
                .Returns("valid order");

            var items = parser.Parse(mockText.Object);

            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void WHEN_ExtractingTextFromAnOrderFails_SHOULD_ThrowException()
        {
            mockOrderLineExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() =>
            {
                parser.Parse(mockText.Object);
            });

            mockOrderLineExtractor.VerifyAll();
        }

        [Fact]
        public void WHEN_ValidatingTextFromAnOrderFails_SHOULD_ThrowException()
        {
            mockOrderLineExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Returns(new List<string>());
            mockOrderLinesValidator
                .Setup(validator => validator.Validate(It.IsAny<List<string>>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() =>
            {
                parser.Parse(mockText.Object);
            });

            mockOrderLineExtractor.VerifyAll();
            mockOrderLinesValidator.VerifyAll();
        }

        [Fact]
        public void WHEN_MatchingAnOrderLineFails_SHOULD_ThrowException()
        {
            mockOrderLineExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Returns(new List<string>() { "valid line" });
            mockOrderLineMatcher
                .Setup(matcher => matcher.Match(It.IsAny<string>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() =>
            {
                parser.Parse(mockText.Object);
            });

            mockOrderLineExtractor.VerifyAll();
            mockOrderLinesValidator.VerifyAll();
        }
    }
}
