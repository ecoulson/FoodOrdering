﻿using System.Text.RegularExpressions;
using Ordering.Service;

namespace Ordering.OrderParser
{
    using System;
    using System.Collections.Generic;
    using Ordering.Order;

    internal class OrderParser: IOrderParser
    {
        private IOrderLineParser orderLineParser;
        private static readonly Regex OrderLinePattern = new Regex("^([0-9]+x){1}\\W+(#[0-9]+|[A-Za-z]{1}[A-Za-z ]*){1}\\W*$", RegexOptions.Multiline);

        public OrderParser(IOrderLineParser orderLineParser)
        {
            this.orderLineParser = orderLineParser;
        }

        public IOrder Parse(IText text)
        {
            Assert.NotNull(text, "[OrderParser] Text to parse should not be null");
            AssertTextIsNotEmpty(text);

            return new Order(new OrderId(), OrderState.Created, ParseOrderLines(text));
        }

        private List<IOrderItem> ParseOrderLines(IText text)
        {
            var result = orderLineParser.Parse(text);
            if (result.HasExceptions())
            {
                throw result.BuildException();
            }
            return result.Items;
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
