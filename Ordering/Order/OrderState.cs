namespace Ordering.Order
{
    public enum OrderState
    {
        Created,
        WaitingForPayment,
        CookingItem,
        WaitingForPickup,
        Completed,
        Error
    }
}
