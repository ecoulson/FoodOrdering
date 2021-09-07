using System;
using FileDatabase.API;
using FileDatabase.File;

namespace FileDatabase.JSON
{
    public interface IJsonReader<T>
    {
        T Read(IFile file);
    }
}
