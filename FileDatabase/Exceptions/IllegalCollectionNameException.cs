using System;
using FileDatabase.Collection;

namespace FileDatabase.Exceptions
{
    public class IllegalCollectionNameException: Exception
    {
        public IllegalCollectionNameException(string name):
            base($"Illegal collection name ${name}. Collection names must be alphabetical")
        {
        }
    }
}
