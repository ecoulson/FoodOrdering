using System;
using FileDatabase.Document;

namespace FileDatabase.API
{
    public interface IFileDatabase<T>
    {
        IDocument<T> CreateDocument(T model);
        IDocument<T> UpdateDocument(IDocumentId id, T model);
        void DeleteDocument(IDocumentId id);
        IDocument<T> ReadDocument(IDocumentId id);
    }
}
