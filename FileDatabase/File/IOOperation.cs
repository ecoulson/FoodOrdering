using System;
using System.Threading.Tasks;

namespace FileDatabase.File
{
    using Common;
    using FileDatabase.Exceptions;

    public class IOOperation: IIOOperation
    {
        public IOperationId Id { get; }
        private Task<IFile> task;
        private bool hasStarted;

        public IFile Result
        {
            get
            {
                if (!hasStarted)
                {
                    throw new OperationNotStartedException(Id);
                }
                return task.GetAwaiter().GetResult();
            }
        }

        public IOOperation(Func<IFile> ioFunction)
        {
            Assert.NotNull(ioFunction, "[IOOperation] io function can not be null");
            task = new Task<IFile>(ioFunction);
            Id = new OperationId();
            hasStarted = false;
        }

        public void Execute()
        {
            try
            {
                hasStarted = true;
                task.Start();
                task.Wait();
                task.Dispose();
            }
            catch (AggregateException exception)
            {
                throw new OperationCanceledException(Id, exception);
            }
            catch (Exception exception)
            {
                throw new OperationFailedException(Id, exception);
            }
        }
    }
}
