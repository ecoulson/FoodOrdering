using System.Collections.Generic;
using Ordering.Order;

namespace Ordering.OrderParser
{
    public interface IOrderLineParseResult
    {
        List<IOrderItem> Items { get; }
        bool HasExceptions();
        IllegalOrderLineException BuildException();
    }
}
