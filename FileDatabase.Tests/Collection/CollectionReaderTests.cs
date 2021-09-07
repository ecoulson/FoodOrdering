using System;
using System.Collections.Generic;
using FileDatabase.API;
using FileDatabase.Collection;
using FileDatabase.Document;
using FileDatabase.File;
using FileDatabase.JSON;
using Moq;
using Xunit;

namespace FileDatabase.Tests.Collection
{
    public class CollectionReaderTests
    {
        private static readonly string DummyId = "ffcb4dcc-86d9-44f6-a131-08022bf504d7";
        private readonly Mock<ICollectionName> mockCollectionName;
        private readonly Mock<IJsonReader<Dictionary<string, IModel>>> mockJsonReader;
        private readonly Mock<IDirectory> mockDirectory;
        private readonly Mock<IFile> mockFile;
        private readonly Mock<IModel> mockModel;
        private readonly ICollectionReader<IModel> collectionReader;

        public CollectionReaderTests()
        {
            mockJsonReader = new Mock<IJsonReader<Dictionary<string, IModel>>>();
            mockFile = new Mock<IFile>();
            mockModel = new Mock<IModel>();
            mockCollectionName = new Mock<ICollectionName>();
            mockDirectory = new Mock<IDirectory>();
            collectionReader = new CollectionReader<IModel>(mockCollectionName.Object, mockJsonReader.Object, mockDirectory.Object);
        }

        [Fact]
        public void WHEN_CreatingACollectionWithNullCollectioName_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionReader<IModel>(null, mockJsonReader.Object, mockDirectory.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingACollectionWithNullJsonReader_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionReader<IModel>(mockCollectionName.Object, null, mockDirectory.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingACollectionWithNullDirectory_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionReader<IModel>(mockCollectionName.Object, mockJsonReader.Object, null);
            });
        }

        [Fact]
        public void WHEN_ReadingAnEmptyCollection_SHOULD_ReturnACollection()
        {
            mockCollectionName
                .Setup(collectionName => collectionName.GetCollectionFile())
                .Returns(mockFile.Object);
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockJsonReader
                .Setup(reader => reader.Read(It.IsAny<IFile>()))
                .Returns(new Dictionary<string, IModel>());

            var collection = collectionReader.Read();

            Assert.NotNull(collection);
        }

        [Fact]
        public void WHEN_ReadingACollectionWithItems_SHOULD_ReturnACollection()
        {
            var dummyData = new Dictionary<string, IModel>() {
                { DummyId, mockModel.Object }
            };
            mockCollectionName
                .Setup(collectionName => collectionName.GetCollectionFile())
                .Returns(mockFile.Object);
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockJsonReader
                .Setup(reader => reader.Read(It.IsAny<IFile>()))
                .Returns(dummyData);

            var collection = collectionReader.Read();

            Assert.True(collection.HasDocument(new DocumentId(DummyId)));
        }
    }
}
