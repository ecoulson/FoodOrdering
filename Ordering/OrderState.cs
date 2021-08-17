namespace Ordering
{
    internal enum OrderState
    {
        WaitingForPayment,
        CookingItem,
        WaitingForPickup,
        Completed,
        Error
    }
}
