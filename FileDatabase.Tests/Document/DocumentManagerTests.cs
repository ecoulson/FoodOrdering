namespace FileDatabase.Tests.Document
{
    using System;
    using FileDatabase.API;
    using FileDatabase.Collection;
    using FileDatabase.Document;
    using FileDatabase.Exceptions;
    using Moq;
    using Xunit;

    public class DocumentManagerTests
    {
        private readonly Mock<ICollectionName> mockDatabaseName;
        private readonly Mock<ICollectionReader<IModel>> mockCollectionReader;
        private readonly Mock<ICollectionWriter<IModel>> mockCollectionWriter;
        private readonly Mock<IDocumentId> mockDocumentId;
        private readonly Mock<IModel> mockModel;
        private readonly Mock<IDocument<IModel>> mockDocument;
        private readonly Mock<ICollection<IModel>> mockCollection;
        private readonly IDocumentManager<IModel> documentManager;

        public DocumentManagerTests()
        {
            mockDatabaseName = new Mock<ICollectionName>();
            mockCollectionReader = new Mock<ICollectionReader<IModel>>();
            mockCollectionWriter = new Mock<ICollectionWriter<IModel>>();
            mockCollection = new Mock<ICollection<IModel>>();
            mockDocument = new Mock<IDocument<IModel>>();
            mockDocumentId = new Mock<IDocumentId>();
            mockModel = new Mock<IModel>();
            documentManager = new DocumentManager<IModel>(
                mockDatabaseName.Object,
                mockCollectionReader.Object,
                mockCollectionWriter.Object
            );
        }

        [Fact]
        public void WHEN_DatabaseNameDependencyIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new DocumentManager<IModel>(null, mockCollectionReader.Object, mockCollectionWriter.Object);
            });
        }

        [Fact]
        public void WHEN_CollectionReaderDependencyIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new DocumentManager<IModel>(mockDatabaseName.Object, null, mockCollectionWriter.Object);
            });
        }

        [Fact]
        public void WHEN_CollectionWriterDependencyIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new DocumentManager<IModel>(mockDatabaseName.Object, mockCollectionReader.Object, null);
            });
        }

        [Fact]
        public void WHEN_CreatingANullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                documentManager.Create(null);
            });
        }

        [Fact]
        public void WHEN_CreatingAModel_SHOULD_ReturnADocument()
        {
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            var document = documentManager.Create(mockModel.Object);

            Assert.NotNull(document);
        }

        [Fact]
        public void WHEN_DeletingWithNullId_SHOULD_ThrowAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                documentManager.Delete(null);
            });
        }

        [Fact]
        public void WHEN_DeletingAndTheIdIsNotFoundInTheCollection_SHOULD_ThrowException()
        {
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            Assert.Throws<DocumentNotFoundException>(() =>
            {
                documentManager.Delete(mockDocumentId.Object);
            });
        }

        [Fact]
        public void WHEN_DeletingADocument_SHOULD_DeleteTheDocument()
        {
            mockCollection
                .Setup(collection => collection.HasDocument(It.IsAny<IDocumentId>()))
                .Returns(true);
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            documentManager.Delete(mockDocumentId.Object);
        }

        [Fact]
        public void WHEN_ReadingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                documentManager.Read(null);
            });
        }

        [Fact]
        public void WHEN_ReadingADocumentWithAnIdThatDoesNotExist_SHOULD_ThrowException()
        {
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            Assert.Throws<DocumentNotFoundException>(() =>
            {
                documentManager.Read(mockDocumentId.Object);
            });
        }

        [Fact]
        public void WHEN_ReadingADocument_SHOULD_ReturnADocument()
        {
            mockCollection
                .Setup(collection => collection.HasDocument(It.IsAny<IDocumentId>()))
                .Returns(true);
            mockCollection
                .Setup(collection => collection.GetDocumentById(It.IsAny<IDocumentId>()))
                .Returns(mockDocument.Object);
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            var document = documentManager.Read(mockDocumentId.Object);

            Assert.NotNull(document);
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                documentManager.Update(null, mockModel.Object);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithNullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                documentManager.Update(mockDocumentId.Object, null);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithAnIdThatDoesNotExist_SHOULD_ThrowException()
        {
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            Assert.Throws<DocumentNotFoundException>(() =>
            {
                documentManager.Update(mockDocumentId.Object, mockModel.Object);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocument_SHOULD_ReturnTheUpdatedDocument()
        {
            mockCollection
                .Setup(collection => collection.HasDocument(It.IsAny<IDocumentId>()))
                .Returns(true);
            mockCollection
                .Setup(collection => collection.GetDocumentById(It.IsAny<IDocumentId>()))
                .Returns(mockDocument.Object);
            mockCollectionReader
                .Setup(reader => reader.Read())
                .Returns(mockCollection.Object);

            var updatedDocument = documentManager.Update(mockDocumentId.Object, mockModel.Object);

            Assert.NotNull(updatedDocument);
        }
    }
}
