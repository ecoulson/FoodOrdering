namespace Ordering.Tests.OrderParser
{
    using System.Collections.Generic;
    using Ordering.OrderParser;
    using Xunit;

    public class OrderLineValidatorTests
    {
        private readonly IOrderLineValidator validator;

        public OrderLineValidatorTests()
        {
            validator = new OrderLineValidator();
        }

        [Theory]
        [InlineData("22ax #31")]
        public void WHEN_ValidatingAnOrderLineWithAnInvalidQuantity_SHOULD_ThrowException(params string[] lines)
        {
            Assert.Throws<IllegalOrderLinesException>(() =>
            {
                validator.Validate(new List<string>(lines));
            });
        }

        [Theory]
        [InlineData("22x 31")]
        [InlineData("22x Chicken09")]
        public void WHEN_ValidatingAnOrderLineWithAnInvalidMenuItemId_SHOULD_ThrowException(params string[] lines)
        {
            Assert.Throws<IllegalOrderLinesException>(() =>
            {
                validator.Validate(new List<string>(lines));
            });
        }

        [Fact]
        public void WHEN_ValidatingAnOrderLineWithNoLines_SHOULD_ThrowAnException()
        {
            Assert.Throws<EmptyOrderException>(() =>
            {
                validator.Validate(new List<string>());
            });
        }

        [Theory]
        [InlineData("22x Chicken Tenders", "81x Pad Tha1")]
        [InlineData("1x #41", "22ax #31")]
        public void WHEN_ValidatingAnInvalidOrderWithMultiplelines_SHOULD_ThrowException(params string[] lines)
        {
            Assert.Throws<IllegalOrderLinesException>(() =>
            {
                validator.Validate(new List<string>(lines));
            });
        }
    }
}
