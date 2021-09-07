using System;
using FileDatabase.File;
using Moq;
using Xunit;

namespace FileDatabase.Tests.File
{
    public class IOSchedulerTests
    {
        private readonly IIOScheduler ioScheduler;
        private readonly Mock<Func<IFile>> mockIOFunction;

        public IOSchedulerTests()
        {
            mockIOFunction = new Mock<Func<IFile>>();
            ioScheduler = new IOScheduler();
        }

        [Fact]
        public void WHEN_RequestingAnOperationWithANullFunction_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ioScheduler.RequestOperation(null);
            });
        }

        [Fact]
        public void WHEN_RequestingAnOperation_SHOULD_ReturnAnIOOperation()
        {
            var operation = ioScheduler.RequestOperation(mockIOFunction.Object);

            Assert.NotNull(operation);
        }
    }
}
