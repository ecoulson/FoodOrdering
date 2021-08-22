using System;
using System.Text.RegularExpressions;
using Ordering.Order;

namespace Ordering.OrderParser
{
    public class OrderLineMatcher: IOrderLineMatcher
    {
        private static int QuantityGroup = 1;
        private static int MenuItemIdentifierGroup = 2;

        public IOrderItem Match(string line)
        {
            Assert.NotNull(line, "[OrderLineMatcher::Match] Can not match a null line");
            AssertLineMatchesPattern(line);

            var match = Constants.OrderLinePattern.Match(line);

            return new OrderItem(
                ParseMenuItemIdentifier(match),
                ParseQuantity(match)
            );
        }

        private void AssertLineMatchesPattern(string line)
        {
            if (!Constants.OrderLinePattern.IsMatch(line))
            {
                throw new IllegalOrderLinesException(line);
            }
        }

        private IMenuItemIdentifier ParseMenuItemIdentifier(Match match)
        {
            return new MenuItemIdentifier(match.Groups[MenuItemIdentifierGroup].Value);
        }

        private IQuantity ParseQuantity(Match match)
        {
            var rawQuantity = match.Groups[QuantityGroup].Value;
            return new Quantity(int.Parse(rawQuantity));
        }
    }
}
