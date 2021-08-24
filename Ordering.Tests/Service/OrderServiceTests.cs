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
            mockOrderParser.VerifyAll();
            mockOrderRepository.VerifyAll();
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
            mockOrderRepository.VerifyAll();
        }
    }
}
