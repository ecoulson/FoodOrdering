using System;
using System.Collections.Generic;

namespace Ordering.OrderParser
{
    public class IllegalOrderLinesException: Exception
    {
        public IllegalOrderLinesException(string formattedIllegalLines) : base($"Could not parse the order: \n\t\"{formattedIllegalLines}\"")
        {
        }
    }
}
