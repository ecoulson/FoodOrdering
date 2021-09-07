using System;
namespace FileDatabase.Exceptions
{
    public class IllegalFilePathException: Exception
    {
        public IllegalFilePathException(string path): base($"File path {path} had illegal characters or did not end with a slash")
        {
        }
    }
}
