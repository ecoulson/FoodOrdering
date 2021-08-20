using System;
using System.Collections.Generic;

namespace Ordering.OrderParser
{
    public interface IParserResult<T>
    {
        List<Exception> Errors { get; }
        T Value { get; }
    }
}
