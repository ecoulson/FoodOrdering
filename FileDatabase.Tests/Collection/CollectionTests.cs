using System;
namespace FileDatabase.Tests.Collection
{
    using FileDatabase.Collection;
    using FileDatabase.API;
    using Xunit;
    using FileDatabase.Document;
    using Moq;
    using FileDatabase.Exceptions;

    public class CollectionTests
    {
        private readonly Mock<ICollectionName> mockCollectionName;
        private readonly Mock<IDocument<IModel>> mockDocument;
        private readonly Mock<IDocumentId> mockDocumentId;
        private readonly ICollection<IModel> collection;

        public CollectionTests()
        {
            mockDocumentId = new Mock<IDocumentId>();
            mockDocument = new Mock<IDocument<IModel>>();
            mockCollectionName = new Mock<ICollectionName>();
            collection = new Collection<IModel>(mockCollectionName.Object);
        }

        [Fact]
        public void WHEN_CreatingACollectionWithNullName_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Collection<IModel>(null);
            });
        }

        [Fact]
        public void WHEN_CheckingIfCollectionHasNullDocumentId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.HasDocument(null);
            });
        }

        [Fact]
        public void WHEN_CheckingIfCollectionHasDocumentId_SHOULD_ReturnFalse()
        {
            Assert.False(collection.HasDocument(mockDocumentId.Object));
        }

        [Fact]
        public void WHEN_AddingANullDocument_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.AddDocument(null);
            });
        }

        [Fact]
        public void WHEN_AddingADocument_SHOULD_AddADocumentAndVerifyItsAddition()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            var document = mockDocument.Object;

            collection.AddDocument(document);

            Assert.True(collection.HasDocument(document.Id));
        }

        [Fact]
        public void WHEN_AddingADocumentWithADuplicateId_SHOULD_ThrowException()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            var document = mockDocument.Object;

            Assert.Throws<DocumentExistsException>(() =>
            {
                collection.AddDocument(document);
                collection.AddDocument(document);
            });
        }

        [Fact]
        public void WHEN_GettingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.GetDocumentById(null);
            });
        }

        [Fact]
        public void WHEN_GettingADocumentThatDoesNotExist_SHOULD_ThrowException()
        {
            Assert.Throws<DocumentNotFoundException>(() =>
            {
                collection.GetDocumentById(mockDocumentId.Object);
            });
        }

        [Fact]
        public void WHEN_GettingADocument_SHOULD_ReturnDocument()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            collection.AddDocument(mockDocument.Object);

            var document = collection.GetDocumentById(mockDocumentId.Object);

            Assert.NotNull(document);
        }

        [Fact]
        public void WHEN_DeletingADocumentThatDoesNotExist_SHOULD_ThrowException()
        {
            Assert.Throws<DocumentNotFoundException>(() =>
            {
                collection.RemoveDocumentById(mockDocumentId.Object);
            });
        }

        [Fact]
        public void WHEN_DeletingADocumentWithNullId_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.RemoveDocumentById(null);
            });
        }

        [Fact]
        public void WHEN_DeletingADocument_SHOULD_NoLongerContainTheDocument()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            collection.AddDocument(mockDocument.Object);

            collection.RemoveDocumentById(mockDocumentId.Object);

            Assert.False(collection.HasDocument(mockDocumentId.Object));
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithNullDocument_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.UpdateDocument(null);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocumentWithAnIdNotInCollection_SHOULD_ThrowException()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);

            Assert.Throws<DocumentNotFoundException>(() =>
            {
                collection.UpdateDocument(mockDocument.Object);
            });
        }

        [Fact]
        public void WHEN_UpdatingADocument_SHOULD_ReturnUpdatedDocument()
        {
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            collection.AddDocument(mockDocument.Object);

            var updatedDocument = collection.UpdateDocument(mockDocument.Object);

            Assert.NotNull(updatedDocument);
        }

        [Fact]
        public void WHEN_IteratingOverACollection_SHOULD_IterateOverEachElement()
        {
            var mockDocument2 = new Mock<IDocument<IModel>>();
            var mockDocumentId2 = new Mock<IDocumentId>();
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            mockDocument2
                .Setup(document => document.Id)
                .Returns(mockDocumentId2.Object);
            collection.AddDocument(mockDocument.Object);
            collection.AddDocument(mockDocument2.Object);

            int count = 0;
            foreach (var document in collection)
            {
                count++;
            }

            Assert.Equal(2, count);
        }
    }
}
