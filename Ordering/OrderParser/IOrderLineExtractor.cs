using System;
using System.Collections.Generic;
using Ordering.Service;

namespace Ordering.OrderParser
{
    public interface IOrderLineExtractor
    {
        List<string> Extract(IText text);
    }
}
