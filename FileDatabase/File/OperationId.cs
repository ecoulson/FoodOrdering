using System;
using Common.Id;

namespace FileDatabase.File
{
    public class OperationId: Id, IOperationId
    {
        public OperationId(): base(Guid.NewGuid().ToString())
        {
        }
    }
}
