namespace Ordering.Tests.Service
{
    using System;
    using Moq;
    using Ordering.Order;
    using Ordering.OrderParser;
    using Ordering.Repository;
    using Ordering.Service;
    using Xunit;

    public class OrderServiceTests
    {
        private readonly Mock<IOrder> mockOrder;
        private readonly Mock<IOrderParser> mockOrderParser;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly IOrderService service;

        private static readonly string DummyOrderId = "a2e02668-5dca-477f-81b4-f0ca2b88042d";

        public OrderServiceTests()
        {
            mockOrderParser = new Mock<IOrderParser>();
            mockOrderRepository = new Mock<IOrderRepository>();
            mockOrder = new Mock<IOrder>();
            service = new OrderService(mockOrderParser.Object, mockOrderRepository.Object);
        }

        [Fact]
        public void WHEN_CreatingAnOrderServiceWithNullParser_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderService(null, mockOrderRepository.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAnOrderServiceWithNullRepository_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderService(mockOrderParser.Object, null);
            });
        }

        [Fact]
        public void WHEN_CreatingAnOrderIt_SHOULD_ReturnAnOrder()
        {
            var mockCreateOrderDto = new Mock<ICreateOrderDto>();
            mockOrderParser
                .Setup(parser => parser.Parse(It.IsAny<IText>()))
                .Returns(mockOrder.Object);
            mockOrderRepository
                .Setup(repository => repository.Create(It.IsAny<IOrder>()))
                .Returns(mockOrder.Object);
            mockCreateOrderDto
                .Setup(dto => dto.Content)
                .Returns("");

            var order = service.CreateOrder(mockCreateOrderDto.Object);

            Assert.NotNull(order);
        }

        [Fact]
        public void WHEN_CreatingAnOrderWithNullDTO_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreateOrder(null);
            });
        }

        [Fact]
        public void WHEN_GettingAnOrderWithNullDTO_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.GetOrder(null);
            });
        }

        [Fact]
        public void WHEN_GettingAnOrder_SHOULD_ReturnAnOrder()
        {
            var mockGetOrderDto = new Mock<IGetOrderDto>();
            mockOrderRepository
                .Setup(repo => repo.Read(It.IsAny<IOrderId>()))
                .Returns(mockOrder.Object);
            mockGetOrderDto
                .Setup(dto => dto.OrderId)
                .Returns(DummyOrderId);

            var order = service.GetOrder(mockGetOrderDto.Object);

            Assert.NotNull(order);
        }

        [Fact]
        public void WHEN_DeletingAnOrderWithNullDto_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.DeleteOrder(null);
            });
        }

        [Fact]
        public void WHEN_DeleteingAnOrder_SHOULD_DeleteTheOrder()
        {
            var mockDeleteOrderDto = new Mock<IDeleteOrderDto>();
            mockDeleteOrderDto
                .Setup(dto => dto.OrderId)
                .Returns(DummyOrderId);
            mockOrderRepository
                .Setup(repo => repo.Delete(It.IsAny<IOrder>()));

            service.DeleteOrder(mockDeleteOrderDto.Object);
        }

        [Fact]
        public void WHEN_UpdatingAnOrderWithNullDTO_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.EditOrder(null);
            });
        }

        [Fact]
        public void WHEN_UpdatingAnOrder_SHOULD_UpdateTheOrder()
        {
            var mockUpdateOrderDto = new Mock<IUpdateOrderDto>();
            mockUpdateOrderDto
                .Setup(dto => dto.Order)
                .Returns(mockOrder.Object);
            mockOrderRepository
                .Setup(repo => repo.Update(It.IsAny<IOrder>()))
                .Returns(mockOrder.Object);

            var updatedOrder = service.EditOrder(mockUpdateOrderDto.Object);

            Assert.NotNull(updatedOrder);
        }
    }
}
