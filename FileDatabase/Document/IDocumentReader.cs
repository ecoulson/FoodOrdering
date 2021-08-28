using System;
using FileDatabase.API;

namespace FileDatabase.Document
{
    public interface IDocumentReader<T>
    {
        IDocument<T> Read(IDatabaseName name, DocumentId id);
    }
}
