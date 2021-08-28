using System;
using FileDatabase.API;

namespace FileDatabase.Document
{
    public interface IDocument
    {
        void Delete(IDatabaseName name, DocumentId id);
    }
}
