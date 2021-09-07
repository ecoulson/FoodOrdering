using System;
using System.Text.RegularExpressions;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.Collection
{
    using FileDatabase.File;

    internal class CollectionName: ICollectionName
    {
        private static readonly Regex CollectionNamePattern =
            new Regex("^[A-Za-z]+$");

        public string Value { get; }

        public CollectionName(string name)
        {
            Assert.NotNull(name, "[CollectionName] name can not be null");
            ValidateName(name);
            Value = name;
        }

        private void ValidateName(string name)
        {
            if (!CollectionNamePattern.IsMatch(name))
            {
                throw new IllegalCollectionNameException(name);
            }
        }

        public IFile GetCollectionFile()
        {
            return new File($"{Value}.json");
        }
    }
}
