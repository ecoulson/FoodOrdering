using System.Text.RegularExpressions;
using Ordering.Service;

namespace Ordering.OrderParser
{    
    using Ordering.Order;

    internal class OrderParser: IOrderParser
    {
        private IOrderLineParser orderLineParser;
        private IPaymentMethodParser paymentMethodParser;
        private static readonly Regex OrderLinePattern = new Regex("^([0-9]+x){1}\\W+(#[0-9]+|[A-Za-z]{1}[A-Za-z ]*){1}\\W*$", RegexOptions.Multiline);

        public OrderParser(IOrderLineParser orderLineParser, IPaymentMethodParser paymentMethodParser)
        {
            this.orderLineParser = orderLineParser;
            this.paymentMethodParser = paymentMethodParser;
        }

        public IOrder Parse(IText text)
        {
            Assert.NotNull(text, "[OrderParser] Text to parse should not be null");
            AssertTextIsNotEmpty(text);

            var orderLinesResult = orderLineParser.Parse(text);
            var paymentMethodResult = paymentMethodParser.Parse(text);

            return new Order(new OrderId(), OrderState.Created, orderLinesResult.Value, paymentMethodResult.Value);
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
