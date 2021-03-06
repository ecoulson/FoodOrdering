using Ordering.Order;

namespace Ordering.Invoice
{
    internal class InvoiceItem: IInvoiceItem
    {
        public IOrderItem OrderItem { get; }
        public ITotal Total { get; }

        public InvoiceItem(IOrderItem orderItem, ITotal total)
        {
            OrderItem = orderItem;
            Total = total;
        }
    }
}
