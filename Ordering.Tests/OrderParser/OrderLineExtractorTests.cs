using System;
using Ordering.OrderParser;

namespace Ordering.Tests.OrderParser
{
    using System.Collections.Generic;
    using Moq;
    using Ordering.Service;
    using Xunit;

    public class OrderLineExtractorTests
    {
        private readonly Mock<IText> mockText;
        private readonly IOrderLineExtractor extractor;

        public OrderLineExtractorTests()
        {
            mockText = new Mock<IText>();
            extractor = new OrderLineExtractor();
        }

        [Fact]
        public void WHEN_ExtractingNullText_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                extractor.Extract(null);
            });
        }

        [Fact]
        public void WHEN_ExtractingTextWithNoOrderLines_SHOULD_ReturnEmptyList()
        {
            mockText
                .Setup(text => text.Content)
                .Returns("some text");

            var result = extractor.Extract(mockText.Object);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("84x Xiao Long Bao")]
        public void WHEN_ExtractingTextWithOnlyOrderLines_SHOULD_ReturnSingleList(string line)
        {
            mockText
                .Setup(text => text.Content)
                .Returns(line);

            var result = extractor.Extract(mockText.Object);

            Assert.Single(result);
            Assert.Equal(line, result[0]);
        }

        [Fact]
        public void WHEN_ExtractingFromEmptyText_SHOULD_ReturnEmptyList()
        {
            mockText
                .Setup(text => text.Content)
                .Returns("");

            var result = extractor.Extract(mockText.Object);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(
            new string[] { "34x Tacos", "Venmo: ecoulson", "12x Catfish" },
            new string[] { "34x Tacos", "12x Catfish" }
        )]
        public void WHEN_ExtractingFromTextWithMixedLines_SHOULD_ReturnListWithExpectedLines(string[] lines, string[] expectedLines)
        {
            mockText
                .Setup(text => text.Content)
                .Returns(string.Join("\n", lines));

            var result = extractor.Extract(mockText.Object);

            AssertExtractedLinesMatchExpectedLines(expectedLines, result);
        }

        private void AssertExtractedLinesMatchExpectedLines(string[] expectedLines, List<string> extractedLines)
        {
            for (int i = 0; i < expectedLines.Length; i++)
            {
                Assert.Equal(expectedLines[i], extractedLines[i]);
            }
        }
    }
}
