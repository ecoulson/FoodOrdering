namespace Ordering.Tests.Repository
{
    using System;
    using FileDatabase.API;
    using FileDatabase.Document;
    using Moq;
    using Ordering.Order;
    using Ordering.Repository;
    using Xunit;

    public class OrderFileRepositoryTests
    {
        private static readonly string DummyOrderId = "a2e02668-5dca-477f-81b4-f0ca2b88042d";

        private readonly Mock<IFileDatabase<IOrderModel>> mockDatabase;
        private readonly Mock<IOrder> mockOrder;
        private readonly Mock<IOrderModel> mockOrderModel;
        private readonly Mock<IDocument<IOrderModel>> mockOrderDocument;
        private readonly Mock<IOrderMapper> mockOrderMapper;
        private readonly IOrderRepository orderRepository;

        public OrderFileRepositoryTests()
        {
            mockDatabase = new Mock<IFileDatabase<IOrderModel>>();
            mockOrder = new Mock<IOrder>();
            mockOrderDocument = new Mock<IDocument<IOrderModel>>();
            mockOrderMapper = new Mock<IOrderMapper>();
            mockOrderModel = new Mock<IOrderModel>();
            orderRepository = new OrderFileRepository(mockDatabase.Object, mockOrderMapper.Object);
        }

        [Fact]
        public void WHEN_CreatingAFileRepositoryWithANullDB_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderFileRepository(null, mockOrderMapper.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileRepositoryWithANullMapper_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OrderFileRepository(mockDatabase.Object, null);
            });
        }

        [Fact]
        public void WHEN_CreatingAnOrderWithNullOrder_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                orderRepository.Create(null);
            });
        }

        [Fact]
        public void WHEN_CreatingAnOrder_SHOULD_ReturnAnOrderWithNewId()
        {
            mockOrder
                .Setup(order => order.SetId(new OrderId(DummyOrderId)));
            mockOrder
                .Setup(order => order.Id())
                .Returns(DummyOrderId);
            mockDatabase
                .Setup(database => database.CreateDocument(It.IsAny<IOrderModel>()))
                .Returns(mockOrderDocument.Object);
            mockOrderDocument
                .Setup(document => document.Id())
                .Returns(DummyOrderId);
            mockOrderMapper
                .Setup(mapper => mapper.ToEntity(It.IsAny<IOrderModel>()))
                .Returns(mockOrder.Object);
            mockOrderMapper
                .Setup(mapper => mapper.ToModel(It.IsAny<IOrder>()))
                .Returns(mockOrderModel.Object);

            var order = orderRepository.Create(mockOrder.Object);

            Assert.Equal(DummyOrderId, order.Id());
            mockDatabase.VerifyAll();
        }

        [Fact]
        public void WHEN_DeletingANullOrder_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                orderRepository.Delete(null);
            });
        }

        [Fact]
        public void WHEN_DeletingAnOrder_SHOULD_ThrowAnException()
        {
            mockOrder
                .Setup(order => order.Id())
                .Returns(DummyOrderId);
            mockOrderMapper
                .Setup(mapper => mapper.ToModel(It.IsAny<IOrder>()))
                .Returns(mockOrderModel.Object);
            mockDatabase
                .Setup(database => database.DeleteDocument(It.IsAny<IDocumentId>()));

            orderRepository.Delete(mockOrder.Object);

            mockDatabase.VerifyAll();
        }

        [Fact]
        public void WHEN_GettingANullOrderId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                orderRepository.Read(null);
            });
        }

        [Fact]
        public void WHEN_GettingAnOrder_SHOULD_ReturnTheOrder()
        {
            var mockOrderId = new Mock<IOrderId>();
            mockOrderId
                .Setup(id => id.ToString())
                .Returns(DummyOrderId);
            mockDatabase
                .Setup(database => database.ReadDocument(It.IsAny<IDocumentId>()))
                .Returns(mockOrderDocument.Object);
            mockOrderMapper
                .Setup(mapper => mapper.ToEntity(It.IsAny<IOrderModel>()))
                .Returns(mockOrder.Object);
            mockOrderDocument
                .Setup(document => document.Id())
                .Returns(DummyOrderId);

            var order = orderRepository.Read(mockOrderId.Object);

            Assert.NotNull(order);
            mockDatabase.VerifyAll();
        }

        [Fact]
        public void WHEN_UpdatingANullOrder_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                orderRepository.Update(null);
            });
        }

        [Fact]
        public void WHEN_UpdatingAnOrder_SHOULD_ReturnUpdatedOrder()
        {
            mockDatabase
                .Setup(database => database.UpdateDocument(
                    It.IsAny<IDocumentId>(),
                    It.IsAny<IOrderModel>()
                ))
                .Returns(mockOrderDocument.Object);
            mockOrderMapper
                .Setup(mapper => mapper.ToModel(It.IsAny<IOrder>()))
                .Returns(mockOrderModel.Object);
            mockOrderMapper
                .Setup(mapper => mapper.ToEntity(It.IsAny<IOrderModel>()))
                .Returns(mockOrder.Object);
            mockOrderDocument
                .Setup(document => document.Id())
                .Returns(DummyOrderId);

            var updatedOrder = orderRepository.Update(mockOrder.Object);

            Assert.NotNull(updatedOrder);
            mockDatabase.VerifyAll();
        }
    }
}
