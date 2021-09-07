using System;
using System.Collections.Generic;
using FileDatabase.API;
using FileDatabase.File;
using FileDatabase.JSON;
using Moq;
using Xunit;

namespace FileDatabase.Tests.JSON
{
    public class JsonWriterTests
    {
        private readonly Mock<IFile> mockFile;
        private readonly Mock<IJsonTransformer> mockJsonTransformer;
        private readonly Mock<IModel> mockModel;
        private readonly Mock<IFileWriter> mockFileWriter;
        private readonly IJsonWriter<IModel> jsonWriter;

        public JsonWriterTests()
        {
            mockJsonTransformer = new Mock<IJsonTransformer>();
            mockFileWriter = new Mock<IFileWriter>();
            mockFile = new Mock<IFile>();
            mockModel = new Mock<IModel>();
            jsonWriter = new JsonWriter<IModel>(mockJsonTransformer.Object, mockFileWriter.Object);
        }

        [Fact]
        public void WHEN_CreatingJsonWriterWithNullTransformer_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new JsonWriter<IModel>(null, mockFileWriter.Object);
            });
        }

        [Fact]
        public void WHEN_CreatingJSONWriterWithNUllFileWriter_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new JsonWriter<IModel>(mockJsonTransformer.Object, null);
            });
        }

        [Fact]
        public void WHEN_WritingJSONWithNullFile_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                jsonWriter.Write(null, mockModel.Object);
            });
        }

        [Fact]
        public void WHEN_WritingJSONWithNullObject_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                jsonWriter.Write(mockFile.Object, null);
            });
        }

        [Fact]
        public void WHEN_WritingJSONWithInvalidFormat_SHOULD_ThrowException()
        {
            mockFile
                .Setup(file => file.HasContent())
                .Returns(true);
            mockFileWriter
                .Setup(reader => reader.Write(It.IsAny<IFile>()))
                .Returns(mockFile.Object);
            mockJsonTransformer
                .Setup(transformer => transformer.Serialize<IModel>(It.IsAny<IModel>()))
                .Throws(new FormatException());

            Assert.Throws<FormatException>(() =>
            {
                jsonWriter.Write(mockFile.Object, mockModel.Object);
            });
        }

        [Fact]
        public void WHEN_WritingJSON_SHOULD_WriteJSON()
        {
            mockFile
                .Setup(file => file.HasContent())
                .Returns(true);
            mockFileWriter
                .Setup(reader => reader.Write(It.IsAny<IFile>()))
                .Returns(mockFile.Object);
            mockJsonTransformer
                .Setup(transformer => transformer.Serialize<IModel>(It.IsAny<IModel>()))
                .Returns("");

            jsonWriter.Write(mockFile.Object, mockModel.Object);
        }
    }
}
