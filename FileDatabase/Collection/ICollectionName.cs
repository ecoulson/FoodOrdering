using FileDatabase.File;

namespace FileDatabase.Collection
{
    public interface ICollectionName
    {
        string Value { get; }
        IFile GetCollectionFile();
    }
}
