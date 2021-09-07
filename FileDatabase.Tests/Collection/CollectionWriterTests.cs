using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace FileDatabase.Tests.Collection
{
    using FileDatabase.API;
    using FileDatabase.Collection;
    using FileDatabase.Document;
    using FileDatabase.File;
    using FileDatabase.JSON;

    public class CollectionWriterTests
    {
        private static readonly string DummyPath = "foo/";
        private static readonly string DummyId = "ffcb4dcc-86d9-44f6-a131-08022bf504d7";

        private readonly Mock<IDocument<IModel>> mockDocument;
        private readonly Mock<ICollectionName> mockCollectionName;
        private readonly Mock<IJsonWriter<Dictionary<string, IModel>>> mockJsonWriter;
        private readonly Mock<IDirectory> mockDirectory;
        private readonly Mock<IFile> mockFile;
        private readonly Mock<IEnumerator<IDocument<IModel>>> mockEnumerator;
        private readonly Mock<ICollection<IModel>> mockCollection;
        private readonly Mock<IDocumentId> mockDocumentId;
        private readonly ICollectionWriter<IModel> collectionWriter;
        private readonly Mock<IModel> mockModel;

        public CollectionWriterTests()
        {
            mockCollectionName = new Mock<ICollectionName>();
            mockDocumentId = new Mock<IDocumentId>();
            mockModel = new Mock<IModel>();
            mockDocument = new Mock<IDocument<IModel>>();
            mockEnumerator = new Mock<IEnumerator<IDocument<IModel>>>();
            mockJsonWriter = new Mock<IJsonWriter<Dictionary<string, IModel>>>();
            mockDirectory = new Mock<IDirectory>();
            mockFile = new Mock<IFile>();
            mockCollection = new Mock<ICollection<IModel>>();
            collectionWriter = new CollectionWriter<IModel>(mockCollectionName.Object, mockJsonWriter.Object, mockDirectory.Object);
        }

        [Fact]
        public void WHEN_CollectionNameIsNUll_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionWriter<IModel>(null, mockJsonWriter.Object, mockDirectory.Object);
            });
        }

        [Fact]
        public void WHEN_JsonWriterIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionWriter<IModel>(mockCollectionName.Object, null, mockDirectory.Object);
            });
        }

        [Fact]
        public void WHEN_DirectoryIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionWriter<IModel>(mockCollectionName.Object, mockJsonWriter.Object, null);
            });
        }

        [Fact]
        public void WHEN_WritingANullCollection_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                collectionWriter.Write(null);
            });
        }

        [Fact]
        public void WHEN_WritingACollection_SHOULD_WriteACollection()
        {
            
            mockCollectionName
                .Setup(collectionName => collectionName.GetCollectionFile())
                .Returns(mockFile.Object);
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockFile
                .Setup(file => file.Path)
                .Returns(DummyPath);
            mockCollection
                .Setup(collection => collection.GetEnumerator())
                .Returns(mockEnumerator.Object);
            mockDocumentId
                .Setup(id => id.Value)
                .Returns(DummyId);
            mockDocument
                .Setup(document => document.Id)
                .Returns(mockDocumentId.Object);
            mockDocument
                .Setup(document => document.Model)
                .Returns(mockModel.Object);
            mockEnumerator
                .Setup(enumerator => enumerator.Current)
                .Returns(mockDocument.Object);
            mockEnumerator
                .Setup(enumerator => enumerator.MoveNext())
                .Returns(new Queue<bool>(new bool[] { true, false }).Dequeue);

            collectionWriter.Write(mockCollection.Object);
        }
    }
}
