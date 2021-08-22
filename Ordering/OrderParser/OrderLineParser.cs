using System.Collections.Generic;
using System.Text.RegularExpressions;
using Menu;
using Ordering.Order;
using Ordering.Service;

namespace Ordering.OrderParser
{
    internal class OrderLineParser: IOrderLineParser
    {
        private readonly IOrderLineExtractor orderLineExtractor;
        private readonly IOrderLineValidator orderLinesValidator;

        public OrderLineParser(IOrderLineExtractor orderLineExtractor, IOrderLineValidator orderLinesValidator)
        {
            this.orderLineExtractor = orderLineExtractor;
            this.orderLinesValidator = orderLinesValidator;
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
            // Todo: Extract Order Line Pattern Match to its own class with the ability to get the amount group and the identifier group
            var parsedLine = Constants.OrderLinePattern.Match(line);
            var quantity = ParseQuantity(parsedLine);
            var menuItemId = ParseMenuItemIdentifier(parsedLine);
            return new OrderItem(menuItemId, quantity);
        }

        private IQuantity ParseQuantity(Match parsedLine)
        {
            var amount = parsedLine.Groups[1].Value.Replace("x", "");
            return new Quantity(int.Parse(amount));
        }

        private IMenuItemIdentifier ParseMenuItemIdentifier(Match parsedLine)
        {
            var id = parsedLine.Groups[2].Value;
            return new MenuItemIdentifier(id);
        }
    }
}
