using System;
using Ordering.Order;
using Ordering.OrderParser;
using Ordering.Repository;

namespace Ordering.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderParser orderParser;
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderParser orderParser, IOrderRepository orderRepository)
        {
            Assert.NotNull(orderParser, "[OrderService] Order parser can not be null");
            this.orderParser = orderParser;
            Assert.NotNull(orderRepository, "[OrderRepository] Order repository can not be null");
            this.orderRepository = orderRepository;
        }

        public IOrder CreateOrder(ICreateOrderDto createOrderDto)
        {
            Assert.NotNull(createOrderDto, "[OrderService::CreateOrder] dto can not be null");
            var order = orderParser.Parse(new Text(createOrderDto.Content));
            return orderRepository.Create(order);
        }

        public void DeleteOrder(IDeleteOrderDto deleteOrderDto)
        {
            // load order
            // delete order
            throw new NotImplementedException();
        }

        public IOrder EditOrder(IOrderId orderId, IOrder orderToUpdate)
        {
            // get order by id
            // run algorithm to update order
            // update order in repo
            throw new NotImplementedException();
        }

        public IOrder GetOrder(IGetOrderDto getOrderDto)
        {
            Assert.NotNull(getOrderDto, "[OrderService::GetOrder] dto can not be null");
            return orderRepository.Read(new OrderId(getOrderDto.OrderId));
        }
    }
}
