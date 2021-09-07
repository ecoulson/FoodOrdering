using System.Collections;
using System.Collections.Generic;
using FileDatabase.API;
using FileDatabase.Document;

namespace FileDatabase.Collection
{
    public interface ICollection<T>: IEnumerable<IDocument<T>> where T: IModel
    {
        void AddDocument(IDocument<T> document);
        bool HasDocument(IDocumentId documentId);
        void RemoveDocumentById(IDocumentId documentId);
        IDocument<T> UpdateDocument(IDocument<T> document);
        IDocument<T> GetDocumentById(IDocumentId documentId);
    }
}
