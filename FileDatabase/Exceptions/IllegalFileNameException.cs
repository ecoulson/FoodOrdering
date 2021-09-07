using System;
namespace FileDatabase.Exceptions
{
    public class IllegalFileNameException: Exception
    {
        public IllegalFileNameException(string name) : base($"File name {name} contained illegal characters or was too long")
        {
        }
    }
}
