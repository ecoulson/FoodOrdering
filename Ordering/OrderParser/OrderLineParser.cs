using System.Collections.Generic;
using Ordering.Order;
using Ordering.Service;

namespace Ordering.OrderParser
{
    internal class OrderLineParser: IOrderLineParser
    {
        private readonly IOrderLineExtractor orderLineExtractor;
        private readonly IOrderLineValidator orderLinesValidator;
        private readonly IOrderLineMatcher orderLineMatcher;

        public OrderLineParser(
            IOrderLineExtractor orderLineExtractor,
            IOrderLineValidator orderLinesValidator,
            IOrderLineMatcher orderLineMatcher
        ) {
            Assert.NotNull(orderLineExtractor, "[OrderLineParser] Line extractor can not be null");
            this.orderLineExtractor = orderLineExtractor;
            Assert.NotNull(orderLinesValidator, "[OrderLineParser] Lines validator can not be null");
            this.orderLinesValidator = orderLinesValidator;
            Assert.NotNull(orderLineMatcher, "[OrderLineParser] Line matcher can not be null");
            this.orderLineMatcher = orderLineMatcher;
        }

        public List<IOrderItem> Parse(IText text)
        {
            Assert.NotNull(text, "[OrderLineParser::Parse] Text can not be null");

            var extractedOrderLines = orderLineExtractor.Extract(text);
            orderLinesValidator.Validate(extractedOrderLines);

            return extractedOrderLines.ConvertAll(ParseExtractedLine);
        }

        private IOrderItem ParseExtractedLine(string line)
        {
            return orderLineMatcher.Match(line);
        }
    }
}
