using System;
using Moq;
using Xunit;

namespace FileDatabase.Tests.File
{
    using FileDatabase.Exceptions;
    using FileDatabase.File;

    public class FileWriterTests
    {
        private static readonly string DummyPath = "foo/";

        private readonly Mock<IIOScheduler> mockIOScheduler;
        private readonly Mock<IFile> mockFile;
        private readonly Mock<IIOOperation> mockIOOperation;
        private readonly IFileWriter fileWriter;

        public FileWriterTests()
        {
            mockIOScheduler = new Mock<IIOScheduler>();
            mockIOOperation = new Mock<IIOOperation>();
            mockFile = new Mock<IFile>();
            fileWriter = new FileWriter(mockIOScheduler.Object);
        }

        [Fact]
        public void WHEN_CreatingAFileWriterWithNullIOSynchronzier()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileWriter(null);
            });
        }

        [Fact]
        public void WHEN_WritingANullFile_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileWriter.Write(null);
            });
        }

        [Fact]
        public void WHEN_WritingAFileWithNoPath_SHOULD_ThrowException()
        {
            Assert.Throws<NoPathException>(() =>
            {
                fileWriter.Write(mockFile.Object);
            });
        }

        [Fact]
        public void WHEN_WritingAFileWithNoContent_SHOULD_ThrowException()
        {
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockFile
                .Setup(file => file.Path)
                .Returns(DummyPath);

            Assert.Throws<NoContentException>(() =>
            {
                fileWriter.Write(mockFile.Object);
            });
        }

        [Fact]
        public void WHEN_WritingFile_SHOULD_WriteFile()
        {
            var mockFileToWrite = new Mock<IFile>();
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockFile
                .Setup(file => file.Path)
                .Returns(DummyPath);
            mockFile
                .Setup(file => file.HasContent())
                .Returns(true);
            mockFile
                .Setup(file => file.Content)
                .Returns("");
            mockIOOperation
                .Setup(operation => operation.Result)
                .Returns(mockFileToWrite.Object);
            mockIOScheduler
                .Setup(synchronizer => synchronizer.RequestOperation(It.IsAny<Func<IFile>>()))
                .Returns(mockIOOperation.Object);

            var result = fileWriter.Write(mockFile.Object);

            Assert.NotNull(result);
        }
    }
}
