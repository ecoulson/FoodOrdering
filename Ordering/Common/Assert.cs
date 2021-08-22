using System;

namespace Ordering
{
    internal class Assert
    {
        public static void NotNull(object o, string message)
        {
            if (o == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void Positive(int i, string message)
        {
            if (i <= 0)
            {
                throw new ArgumentOutOfRangeException(message);
            }
        }

        public static void NotNegative(int i, string message)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException(message);
            }
        }
    }
}
