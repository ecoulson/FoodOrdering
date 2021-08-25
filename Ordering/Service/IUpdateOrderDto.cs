using System;
using Ordering.Order;

namespace Ordering.Service
{
    public interface IUpdateOrderDto
    {
        IOrder Order { get; set; }
    }
}
