using System;
using System.Collections.Generic;

namespace Ordering.OrderParser
{
    public interface IOrderLineValidator
    {
        void Validate(List<string> extractedLines);
    }
}
