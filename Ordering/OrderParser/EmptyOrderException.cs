using System;
namespace Ordering.OrderParser
{
    public class EmptyOrderException: Exception
    {
        public EmptyOrderException(): base("Can not parse an empty order")
        {
        }
    }
}
