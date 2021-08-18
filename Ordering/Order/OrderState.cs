namespace Ordering.Order
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
