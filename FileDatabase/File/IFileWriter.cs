using System;
namespace FileDatabase.File
{
    public interface IFileWriter
    {
        IFile Write(IFile file);
    }
}
