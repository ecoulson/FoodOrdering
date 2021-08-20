using System;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IParser<T>
    {
        IParserResult<T> Parse(IText text);
    }
}
