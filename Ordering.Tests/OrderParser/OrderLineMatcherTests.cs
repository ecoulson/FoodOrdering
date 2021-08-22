namespace Ordering.Tests.OrderParser
{
    using Ordering.OrderParser;
    using Xunit;

    public class OrderLineMatcherTests
    {
        private IOrderLineMatcher matcher;

        public OrderLineMatcherTests()
        {
            matcher = new OrderLineMatcher();
        }

        [Fact]
        public void WHEN_MatchingAnOrderLineTestThatDoesNotMatchThePattern_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalOrderLinesException>(() =>
            {
                matcher.Match("random string");
            });
        }

        [Fact]
        public void WHEN_MatchingAValidOrderLine_SHOULD_ReturnAnOrderItem()
        {
            var orderItem = matcher.Match("22x Chicken Tenders");

            Assert.Equal(22, orderItem.Quantity());
            Assert.Equal("Chicken Tenders", orderItem.MenuItemId());
        }
    }
}
