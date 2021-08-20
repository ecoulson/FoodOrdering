using System;
namespace Ordering.OrderParser
{
    public class IllegalOrderLineException: Exception
    {
        public IllegalOrderLineException(string[] lines) : base("Could not parse the order: \n\t\"" + lines + "\"")
        {
        }
    }
}
