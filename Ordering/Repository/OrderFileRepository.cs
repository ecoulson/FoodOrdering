using System;
using FileDatabase.API;
using FileDatabase.Document;
using Ordering.Order;

namespace Ordering.Repository
{
    internal class OrderFileRepository: IOrderRepository
    {
        private readonly IFileDatabase<IOrderModel> orderDatabase;
        private readonly IOrderMapper orderMapper;

        public OrderFileRepository(IFileDatabase<IOrderModel> orderDatabase, IOrderMapper orderMapper)
        {
            Assert.NotNull(orderDatabase, "[OrderFileRepository] database can not be null");
            this.orderDatabase = orderDatabase;
            Assert.NotNull(orderMapper, "[OrderFileRepository] mapper can not be null");
            this.orderMapper = orderMapper;
        }

        public IOrder Create(IOrder order)
        {
            Assert.NotNull(order, "[OrderFileRepository::Create] order can not be null");
            var model = orderMapper.ToModel(order);
            var document = orderDatabase.CreateDocument(model);
            return GetOrderFromDocument(document);
        }

        public void Delete(IOrder order)
        {
            Assert.NotNull(order, "[OrderFileRepository::Delete] order can not be null");
            orderDatabase.DeleteDocument(new DocumentId(order.Id));
        }

        public IOrder Read(IOrderId orderId)
        {
            Assert.NotNull(orderId, "[OrderFileRepository::Read] order id can not be null");
            var document = orderDatabase.ReadDocument(new DocumentId(orderId));
            return GetOrderFromDocument(document);
        }

        private IOrder GetOrderFromDocument(IDocument<IOrderModel> document)
        {
            var order = orderMapper.ToEntity(document.Model);
            order.Id = new OrderId(document.Id.Value);
            return order;
        }


        public IOrder Update(IOrder order)
        {
            Assert.NotNull(order, "[OrderFileRepository::Update] order can not be null");
            var document = orderDatabase.UpdateDocument(
                new DocumentId(order.Id),
                orderMapper.ToModel(order)
            );
            return GetOrderFromDocument(document);
        }
    }
}
