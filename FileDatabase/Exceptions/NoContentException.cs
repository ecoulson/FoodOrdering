using System;
using FileDatabase.File;

namespace FileDatabase.Exceptions
{
    public class NoContentException: Exception
    {
        public NoContentException(IFile file) : base($"File {file.Name} does not have any content")
        {
        }
    }
}
