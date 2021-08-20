using System;
namespace Ordering.OrderParser
{
    public class IllegalPaymentMethodException: Exception
    {
        public IllegalPaymentMethodException(string paymentLine) : base($"Could not parse payment method from line {paymentLine}")
        {
        }
    }
}
