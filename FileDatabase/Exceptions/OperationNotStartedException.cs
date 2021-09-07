using System;
using FileDatabase.File;

namespace FileDatabase.Exceptions
{
    public class OperationNotStartedException: Exception
    {
        public OperationNotStartedException(IOperationId id)
            : base($"Operation {id.Value} has not been started yet, can not await for a result")
        {
        }
    }
}
