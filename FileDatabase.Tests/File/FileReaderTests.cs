using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FileDatabase.Exceptions;
using FileDatabase.File;
using Moq;
using Xunit;

namespace FileDatabase.Tests.File
{
    public class FileReaderTests
    {
        private static readonly string DummyPath = "foo/";

        private readonly Mock<IFile> mockFile;
        private readonly Mock<IIOScheduler> mockIOScheduler;
        private readonly Mock<IIOOperation> mockIOOperation;
        private readonly IFileReader fileReader;

        public FileReaderTests()
        {
            mockFile = new Mock<IFile>();
            mockIOScheduler = new Mock<IIOScheduler>();
            mockIOOperation = new Mock<IIOOperation>();
            fileReader = new FileReader(mockIOScheduler.Object);
        }

        [Fact]
        public void WHEN_FileSynthesizerIsNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileReader(null);
            });
        }

        [Fact]
        public void WHEN_ReadingANullFile_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileReader.Read(null);
            });
        }

        [Fact]
        public void WHEN_ReadingAFileWithNoPath_SHOULD_ThrowException()
        {
            Assert.Throws<NoPathException>(() =>
            {
                fileReader.Read(mockFile.Object);
            });
        }

        [Fact]
        public void WHEN_ReadingAFile_SHOULD_ReturnContentsOfFile()
        {
            var mockFileToRead = new Mock<IFile>();
            mockFileToRead
                .Setup(file => file.Content)
                .Returns("");
            mockFile
                .Setup(file => file.HasPath())
                .Returns(true);
            mockFile
                .Setup(file => file.Path)
                .Returns(DummyPath);
            mockIOOperation
                .Setup(operation => operation.Result)
                .Returns(mockFileToRead.Object);
            mockIOScheduler
                .Setup(synchronizer => synchronizer.RequestOperation(It.IsAny<Func<IFile>>()))
                .Returns(mockIOOperation.Object);

            var readFile = fileReader.Read(mockFile.Object);

            Assert.Empty(readFile.Content);
        }
    }
}
