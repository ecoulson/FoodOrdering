using System;
namespace FileDatabase.File
{
    public interface IFileReader
    {
        IFile Read(IFile file);
    }
}
