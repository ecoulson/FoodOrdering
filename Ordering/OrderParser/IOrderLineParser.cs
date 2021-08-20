using System;
using System.Collections.Generic;
using Ordering.Order;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IOrderLineParser : IParser<List<IOrderItem>>
    {
    }
}
