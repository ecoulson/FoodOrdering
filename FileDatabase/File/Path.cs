using System;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.File
{
    public class Path: IPath
    {
        public string Value { get; }

        public Path(string path)
        {
            Assert.NotNull(path, "[Path] path string can not be null");
            ValidatePath(path);
            Value = path;
        }

        private void ValidatePath(string path)
        {
            AssertValidPathLength(path);
            AssertFilePathHasValidCharacters(path);
            AssertPathEndsWithSlash(path);
        }

        private void AssertValidPathLength(string path)
        {
            if (path.Length > FileConstants.MaxPathLength)
            {
                throw new IllegalFilePathException(path);
            }
        }

        private void AssertFilePathHasValidCharacters(string path)
        {
            if (StringContainsReservedCharacters(path))
            {
                throw new IllegalFilePathException(path);
            }
        }

        private void AssertPathEndsWithSlash(string path)
        {
            if (!path.EndsWith("/"))
            {
                throw new IllegalFilePathException(path);
            }
        }

        private bool StringContainsReservedCharacters(string fileString)
        {
            foreach (var reservedChar in FileConstants.ReservedCharacters)
            {
                if (fileString.Contains(reservedChar))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
