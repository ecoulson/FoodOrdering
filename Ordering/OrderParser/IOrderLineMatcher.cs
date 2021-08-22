using System;
using Ordering.Order;

namespace Ordering.OrderParser
{
    public interface IOrderLineMatcher
    {
        IOrderItem Match(string line);
    }
}
