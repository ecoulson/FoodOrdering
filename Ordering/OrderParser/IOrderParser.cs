using Ordering.Order;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IOrderParser
    {
        IOrder Parse(IText text);
    }
}
