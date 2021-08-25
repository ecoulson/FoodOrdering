using System;
namespace FileDatabase.API
{
    public interface IDocument<T>
    {
        string Id();
        T Model { get; }
    }
}
