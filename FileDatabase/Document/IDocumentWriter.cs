using System;
using FileDatabase.API;

namespace FileDatabase.Document
{
    public interface IDocumentWriter<T>
    {
        IDocument<T> Write(IDatabaseName name, IDocumentId id, T model);
    }
}
