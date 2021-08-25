using System;
namespace FileDatabase.API
{
    public interface IFileDatabase<T>
    {
        IDocument<T> CreateDocument(T model);
        IDocument<T> UpdateDocument(string id, T model);
        void DeleteDocument(T model);
        IDocument<T> ReadDocument(string id);
    }
}
