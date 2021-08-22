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
        private readonly Mock<IOrderLineExtractor> mockExtractor;
        private readonly Mock<IOrderLineValidator> mockValidator;
        private readonly IOrderLineParser parser;

        public OrderLineParserTests()
        {
            mockText = new Mock<IText>();
            mockExtractor = new Mock<IOrderLineExtractor>();
            mockValidator = new Mock<IOrderLineValidator>();
            parser = new OrderLineParser(mockExtractor.Object, mockValidator.Object);
        }

        [Fact]
        public void WHEN_ParsingAnOrderLineWithANullText_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                parser.Parse(null);
            });
        }

        [Theory]
        [InlineData(
            new string[] { "22x Chicken Tenders" },
            new string[] { "Chicken Tenders" },
            new int[] { 22 }
        )]
        [InlineData(
            new string[] { "1x #41" },
            new string[] { "#41" },
            new int[] { 1 }
        )]
        [InlineData(
            new string[] { "22x Chicken Tenders", "81x #31", "2x Tacos" },
            new string[] { "Chicken Tenders", "#31", "Tacos" },
            new int[] { 22, 81, 2 }
        )]
        [InlineData(
            new string[] { "1x #41", "22x #31", "2x Tacos" },
            new string[] { "#41", "#31", "Tacos" },
            new int[] { 1, 22, 2 }
        )]
        public void WHEN_ParsingAValidOrderWithMultiplelines_SHOULD_ReturnMultipleOrderItems(
            string[] lines,
            string[] expectedIds,
            int[] expectedQuantities
        ) {
            AssertValidTestData(lines, expectedIds, expectedQuantities);
            mockExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Returns(new List<string>(lines));
            mockText
                .Setup(text => text.Content)
                .Returns(string.Join("\n", lines));

            var items = parser.Parse(mockText.Object);

            AssertItemsHaveExpectedValues(items, expectedIds, expectedQuantities);
        }

        private void AssertValidTestData(string[] lines, string[] expectedIds, int[] expectedQuantities)
        {
            if (lines.Length != expectedIds.Length ||
                lines.Length != expectedQuantities.Length ||
                expectedIds.Length != expectedQuantities.Length)
            {
                throw new Exception("Invalid test data. Check your inline data");
            }
        }

        private void AssertItemsHaveExpectedValues(List<IOrderItem> items, string[] expectedIds, int[] expectedQuantities)
        {
            var mockedExpectedOrderItems = GetExpectedOrderItems(expectedIds, expectedQuantities);
            for (int i = 0; i < items.Count; i++)
            {
                AssertItemHasExpectedValues(mockedExpectedOrderItems[i], items[i]);
            }
        }

        private void AssertItemHasExpectedValues(Mock<IOrderItem> expectedItem, IOrderItem item)
        {
            Assert.NotNull(item);
            Assert.True(expectedItem.Object.Equals(item));
            expectedItem.VerifyAll();
        }

        private List<Mock<IOrderItem>> GetExpectedOrderItems(string[] expectedIds, int[] expectedQuantities)
        {
            var result = new List<Mock<IOrderItem>>();
            for (int i = 0; i < expectedIds.Length; i++)
            {
                result.Add(GetMockOrderItem(expectedIds[i], expectedQuantities[i]));
            }
            return result;
        }

        private Mock<IOrderItem> GetMockOrderItem(string expectedId, int expectedQuantity)
        {
            var mockOrderItem = new Mock<IOrderItem>();
            mockOrderItem
                .Setup(item => item.Equals(It.IsAny<IOrderItem>()))
                .Returns((IOrderItem item) => {
                    return item.Quantity() == expectedQuantity &&
                        item.MenuItemId() == expectedId;
                });
            return mockOrderItem;
        }

        [Fact]
        public void WHEN_ExtractingTextFromAnOrderFails_SHOULD_ThrowException()
        {
            mockExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() =>
            {
                parser.Parse(mockText.Object);
            });

            mockExtractor.VerifyAll();
        }

        [Fact]
        public void WHEN_ValidatingTextFromAnOrderFails_SHOULD_ThrowException()
        {
            mockExtractor
                .Setup(extractor => extractor.Extract(It.IsAny<IText>()))
                .Returns(new List<string>());
            mockValidator
                .Setup(validator => validator.Validate(It.IsAny<List<string>>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() =>
            {
                parser.Parse(mockText.Object);
            });

            mockExtractor.VerifyAll();
            mockValidator.VerifyAll();
        }
    }
}
