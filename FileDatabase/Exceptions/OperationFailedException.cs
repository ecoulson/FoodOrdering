using System;
using FileDatabase.File;

namespace FileDatabase.Exceptions
{
    public class OperationFailedException: Exception
    {
        public OperationFailedException(IOperationId operationId, Exception innerException)
            :base($"Operation {operationId.Value} failed", innerException)
        {

        }
    }
}
