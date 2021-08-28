namespace FileDatabase.Tests.API
{
    using System;
    using FileDatabase.API;
    using FileDatabase.Document;
    using FileDatabase.Exceptions;
    using Moq;
    using Xunit;

    public class FileDatabaseTests
    {
        public class TestModel { }

        private readonly Mock<IDocumentManager<TestModel>> mockDocumentManager;
        private readonly Mock<IDocument<TestModel>> mockDocument;
        private readonly Mock<IDocumentId> mockDocumentId;
        private readonly FileDatabase<TestModel> fileDatabase;

        public FileDatabaseTests()
        {
            mockDocumentManager = new Mock<IDocumentManager<TestModel>>();
            mockDocument = new Mock<IDocument<TestModel>>();
            mockDocumentId = new Mock<IDocumentId>();
            fileDatabase = new FileDatabase<TestModel>(mockDocumentManager.Object);
        }

        [Fact]
        public void WHEN_CreatingADocumentWithNullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabase.CreateDocument(null);
            });
        }

        [Fact]
        public void WHEN_CreatingADocument_SHOULD_ReturnAndWriteDocument()
        {
            mockDocumentManager
                .Setup(manager => manager.Create(It.IsAny<TestModel>()))
                .Returns(mockDocument.Object);

            var document = fileDatabase.CreateDocument(new TestModel());

            Assert.NotNull(document);
        }

        [Fact]
        public void WHEN_ReadingADocumentWithNullDocumentId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabase.ReadDocument(null);
            });
        }

        [Fact]
        public void WHEN_ReadingADocument_SHOULD_ReturnADocument()
        {
            mockDocumentManager
                .Setup(manager => manager.Read(It.IsAny<IDocumentId>()))
                .Returns(mockDocument.Object);

            var document = fileDatabase.ReadDocument(mockDocumentId.Object);

            Assert.NotNull(document);
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabase.UpdateDocument(null, new TestModel());
            });
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithNullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabase.UpdateDocument(mockDocumentId.Object, null);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocument_SHOULD_ReturnUpdatedDocument()
        {
            mockDocumentManager
                .Setup(manager => manager.Update(It.IsAny<IDocumentId>(), It.IsAny<TestModel>()))
                .Returns(mockDocument.Object);

            var updatedDocument = fileDatabase.UpdateDocument(mockDocumentId.Object, new TestModel());

            Assert.NotNull(updatedDocument);
        }

        [Fact]
        public void WHEN_DeletingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabase.DeleteDocument(null);
            });
        }

        [Fact]
        public void WHEN_DeletingADocument_SHOULD_DeleteDocument()
        {
            mockDocumentManager
                .Setup(manager => manager.Delete(It.IsAny<IDocumentId>()));

            fileDatabase.DeleteDocument(mockDocumentId.Object);
        }
    }
}
