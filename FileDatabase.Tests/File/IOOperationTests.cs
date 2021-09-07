using System;
using FileDatabase.File;
using Moq;
using Xunit;

namespace FileDatabase.Tests.File
{
    using FileDatabase.Exceptions;

    public class IOOperationTests
    {
        private Mock<IFile> mockFile;
        private Mock<Func<IFile>> mockFunction;
        private IIOOperation ioOperation;

        public IOOperationTests()
        {
            mockFile = new Mock<IFile>();
            mockFunction = new Mock<Func<IFile>>();
            ioOperation = new IOOperation(mockFunction.Object);
        }

        [Fact]
        public void WHEN_OperationIsCreatedWithNullIOFunction_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new IOOperation(null);
            });
        }

        [Fact]
        public void WHEN_OperationFails_SHOULD_ThrowException()
        {
            mockFunction
                .Setup(func => func.Invoke())
                .Throws(new ArgumentOutOfRangeException());

            Assert.Throws<OperationCanceledException>(() =>
            {
                ioOperation.Execute();
            });
        }

        [Fact]
        public void WHEN_GettingResult_SHOULD_GetResult()
        {
            mockFunction
                .Setup(func => func.Invoke())
                .Returns(mockFile.Object);
            ioOperation.Execute();

            var result = ioOperation.Result;

            Assert.NotNull(result);
            Assert.NotNull(ioOperation.Id);
        }

        [Fact]
        public void WHEN_GettingResultOfUnstartedTask_SHOULD_ThrowException()
        {
            Assert.Throws<OperationNotStartedException>(() =>
            {
                var result = ioOperation.Result;
            });
        }
    }
}
