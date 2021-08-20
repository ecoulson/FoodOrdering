using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IOrderLineParser
    {
        IOrderLineParseResult Parse(IText text);
    }
}
