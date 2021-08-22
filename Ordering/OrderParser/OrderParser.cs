using Ordering.Service;

namespace Ordering.OrderParser
{
    using System.Collections.Generic;
    using Ordering.Order;

    internal class OrderParser: IOrderParser
    {
        private readonly IOrderLineParser orderLineParser;

        public OrderParser(IOrderLineParser orderLineParser)
        {
            this.orderLineParser = orderLineParser;
        }

        public IOrder Parse(IText text)
        {
            Assert.NotNull(text, "[OrderParser] Text to parse should not be null");
            AssertTextIsNotEmpty(text);

            return new Order(new OrderId(), OrderState.Created, orderLineParser.Parse(text));
        }

        private void AssertTextIsNotEmpty(IText text)
        {
            if (text.IsEmpty())
            {
                throw new EmptyOrderException();
            }
        }
    }
}
