using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace FileDatabase.File
{
    public class IOScheduler: IIOScheduler
    {
        private static readonly BlockingCollection<IIOOperation> operations = new BlockingCollection<IIOOperation>();

        public void StartSchedulingOperations()
        {
            new Thread(HandleOperations).Start();
        }

        private void HandleOperations()
        {
            foreach (var operation in operations.GetConsumingEnumerable())
            {
                operation.Execute();
            }
        }

        public IIOOperation RequestOperation(Func<IFile> ioFunction)
        {
            Assert.NotNull(ioFunction, "[FileSynchronizer::RequestOperation] ioFunction can not be null");
            var operation = new IOOperation(ioFunction);
            operations.Add(operation);
            return operation;
        }
    }
}
