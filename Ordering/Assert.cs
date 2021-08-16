using System;
namespace Ordering
{
    internal class Assert
    {
        public static void NotNull(Object o, string message)
        {
            if (o == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}
