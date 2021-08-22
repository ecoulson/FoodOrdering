using System;
using System.Collections.Generic;
using Ordering.Order;

namespace Ordering.OrderParser
{
    public class OrderLineParseResult: IOrderLineParseResult
    {
        private readonly List<IOrderItem> orderItems;
        private readonly List<string> invalidLines;

        public OrderLineParseResult(List<IOrderItem> orderItems, List<string> invalidLines)
        {
            this.orderItems = orderItems;
            this.invalidLines = invalidLines;
        }

        public List<IOrderItem> Items => orderItems;

        public IllegalOrderLinesException BuildException()
        {
            return new IllegalOrderLinesException("");
        }

        public bool HasExceptions()
        {
            return invalidLines.Count > 0;
        }
    }
}
