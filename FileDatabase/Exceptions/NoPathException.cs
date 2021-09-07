using System;
using FileDatabase.File;

namespace FileDatabase.Exceptions
{
    public class NoPathException: Exception
    {
        public NoPathException(IFile file): base($"File {file.Name} does not have a path")
        {
        }
    }
}
