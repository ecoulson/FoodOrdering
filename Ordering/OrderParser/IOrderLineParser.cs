using System.Collections.Generic;
using Ordering.Order;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IOrderLineParser
    {
        List<IOrderItem> Parse(IText text);
    }
}
