using System;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.File
{
    public class FileName: IFileName
    {
        public string Value { get; }

        public FileName(string name)
        {
            Assert.NotNull(name, "[FileName] name can not be null");
            ValidateFileName(name);
            Value = name;
        }

        private void ValidateFileName(string fileName)
        {
            AssertFileNameLengthIsInRange(fileName);
            AssertFileNameDoesNotContainPathSeperator(fileName);
            AssertFileNameHasValidCharacters(fileName);
        }

        private void AssertFileNameLengthIsInRange(string fileName)
        {
            if (fileName.Length > FileConstants.MaxFileNameLength)
            {
                throw new IllegalFileNameException(fileName);
            }
        }

        private void AssertFileNameDoesNotContainPathSeperator(string fileName)
        {
            if (fileName.Contains(FileConstants.PathSeperator))
            {
                throw new IllegalFileNameException(fileName);
            }
        }

        private void AssertFileNameHasValidCharacters(string fileName)
        {
            if (StringContainsReservedCharacters(fileName))
            {
                throw new IllegalFileNameException(fileName);
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
