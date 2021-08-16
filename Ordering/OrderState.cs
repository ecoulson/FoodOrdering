using System;
namespace Ordering
{
    public enum OrderState
    {
        WaitingForPayment,
        CookingItem,
        WaitingForPickup,
        Completed,
        Error
    }
}
