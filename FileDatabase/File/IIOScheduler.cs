using System;
using System.Threading.Tasks;

namespace FileDatabase.File
{
    public interface IIOScheduler
    {
        void StartSchedulingOperations();
        IIOOperation RequestOperation(Func<IFile> readFileFunction);
    }
}
