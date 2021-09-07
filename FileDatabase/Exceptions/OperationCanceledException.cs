using System;
using FileDatabase.File;

namespace FileDatabase.Exceptions
{
    public class OperationCanceledException: Exception
    {
        public OperationCanceledException(IOperationId operationId, Exception innerException)
            :base($"Operation {operationId.Value} was canceled", innerException)
        {

        }
    }
}
