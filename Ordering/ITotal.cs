using System;
namespace Ordering
{
    public interface ITotal
    {
        int Cost { get; }
        void AddToTotal(IOrderItem item);
    }
}
