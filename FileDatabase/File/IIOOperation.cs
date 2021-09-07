using System;
using System.Threading.Tasks;

namespace FileDatabase.File
{
    public interface IIOOperation
    {
        IOperationId Id { get; }
        IFile Result { get; }
        void Execute();
    }
}
