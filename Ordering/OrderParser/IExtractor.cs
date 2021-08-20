using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IExtractor
    {
        string[] Extract(IText text);
    }
}
