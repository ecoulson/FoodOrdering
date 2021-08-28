using System;
namespace FileDatabase.Document
{
    public interface IDocument<T>
    {
        string Id();
        T Model { get; }
    }
}
