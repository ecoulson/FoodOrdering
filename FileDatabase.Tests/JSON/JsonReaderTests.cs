using System;
using System.Collections.Generic;
using FileDatabase.File;
using FileDatabase.JSON;
using Moq;
using Xunit;

namespace FileDatabase.Tests.JSON
{
    public class JsonReaderTests
    {
        private readonly Mock<IJsonTransformer> mockJsonTransformer;
        private readonly Mock<IFile> mockFile;
        private readonly Mock<IFileReader> mockFileReader;
        private readonly IJsonReader<List<string>> jsonReader;

        public JsonReaderTests()
        {
            mockJsonTransformer = new Mock<IJsonTransformer>();
            mockFile = new Mock<IFile>();
            mockFileReader = new Mock<IFileReader>();
            jsonReader = new JsonReader<List<string>>(mockJsonTransformer.Object, mockFileReader.Object);
        }

        [Fact]
        public void WHEN_CreatingAJSONFileReaderWithNullJSONTransformer_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new JsonReader<List<string>>(null, mockFileReader.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingAJSONFileReaderWithNullFileReader_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new JsonReader<List<string>>(mockJsonTransformer.Object, null);
            });
        }

        [Fact]
        public void WHEN_ReadingANullFile_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                jsonReader.Read(null);
            });
        }

        [Fact]
        public void WHEN_ReadingAJSONFileWithInvalidJSON_SHOULD_ThrowException()
        {
            mockFile
                .Setup(file => file.HasContent())
                .Returns(true);
            mockFileReader
                .Setup(reader => reader.Read(It.IsAny<IFile>()))
                .Returns(mockFile.Object);
            mockJsonTransformer
                .Setup(transformer => transformer.Deserialize<List<string>>(It.IsAny<string>()))
                .Throws(new FormatException());

            Assert.Throws<FormatException>(() =>
            {
                jsonReader.Read(mockFile.Object);
            });
        }

        [Fact]
        public void WHEN_ReadingAJSONFile_SHOULD_ReturnJSON()
        {
            mockFile
                .Setup(file => file.HasContent())
                .Returns(true);
            mockFileReader
                .Setup(reader => reader.Read(It.IsAny<IFile>()))
                .Returns(mockFile.Object);
            mockJsonTransformer
                .Setup(transformer => transformer.Deserialize<List<string>>(It.IsAny<string>()))
                .Returns(new List<string>() { "a", "b", "c" });

            var list = jsonReader.Read(mockFile.Object);

            Assert.Equal(3, list.Count);
        }
    }
}
