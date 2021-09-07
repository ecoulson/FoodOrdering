using System;
using FileDatabase.File;

namespace FileDatabase.JSON
{
    public interface IJsonWriter<T>
    {
        void Write(IFile file, T obj);
    }
}
